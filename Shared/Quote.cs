﻿namespace Shared
{
    public class Quote
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public List<string> Tags { get; set; }
        public string QuoteText { get; set; }
    }
}
