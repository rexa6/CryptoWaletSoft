using System.Net.Http;
using System.Text.Json;

namespace WpfApp4
{
    public class TransferParser
    {
        private readonly HttpClient _httpClient = new();
        private CancellationTokenSource _cts;

        public event Action<List<Transfer>> OnNewTransfers;  // событие для новых данных
        public event Action<string> OnError;                 // событие для ошибок

        public bool IsRunning => _cts != null && !_cts.IsCancellationRequested;

        public void StartParsing()
        {
            if (IsRunning) return;

            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            Task.Run(async () =>
            {
                int start = 0;
                const int limit = 10;

                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        string url = $"https://apilist.tronscanapi.com/api/transfer?sort=-timestamp&count=true&limit={limit}&start={start}";
                        string json = await _httpClient.GetStringAsync(url);

                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var result = JsonSerializer.Deserialize<TransferResponse>(json, options);

                        if (result?.data == null || result.data.Count == 0)
                        {
                            await Task.Delay(1000, token);
                            continue;
                        }

                        OnNewTransfers?.Invoke(result.data);

                        start += limit;
                        await Task.Delay(1000, token);
                    }
                }
                catch (OperationCanceledException) { }
                catch (Exception ex)
                {
                    OnError?.Invoke(ex.Message);
                }
            }, token);
        }

        public void StopParsing()
        {
            if (IsRunning)
            {
                _cts.Cancel();
                _cts = null;
            }
        }
    }
}
