using MemoryGame.Models;

namespace MemoryGame.Infra
{
    public class RecordDecorator : Record
    {
        public bool IsGuessed { get; set; }
        private readonly Record _record;
        public RecordDecorator(Record record)
        {
            _record = record;
        }
    }
}
