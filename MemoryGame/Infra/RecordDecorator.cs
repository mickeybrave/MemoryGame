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

        public int ID { get { return _record.ID; } }
        public string Word { get { return _record.Word; } }
        public string Translation { get { return _record.Translation; } }
    }
}
