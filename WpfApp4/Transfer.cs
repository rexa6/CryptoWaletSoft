using System.Collections.Generic;

namespace WpfApp4
{
    public class Transfer
    {
        public string transferFromAddress { get; set; }
        public string transferToAddress { get; set; }
    }

    public class TransferResponse
    {
        public List<Transfer> data { get; set; }
    }
}
