using FlashcardsAPI.Models;

namespace FlashcardsAPI.Dtos
{
    public class BulkImportRequest
    {
        public List<CardDto> Cards { get; set; }
    }
}
