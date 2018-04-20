namespace SharedModels
{
    public class SubmitResultModel
    {
        /// <summary>
        /// enqueued
        /// not queued - Error 155
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 0
        /// 200674274 (example)
        /// </summary>
        public long Key { get; set; }

        /// <summary>
        /// 1
        /// 0
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// 27830000000 (example)
        /// </summary>
        public long Number { get; set; }

        /// <summary>
        /// 155 (duplicate)
        /// </summary>
        public int Error { get; set; }
    }
}
