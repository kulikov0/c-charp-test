using System;
using System.Collections.Generic;
using System.Text;

namespace KDZCore.CloudDataSource.Model
{
    public class LessonResponse
    {
        public string building { get; set; }
        public string type { get; set; }
        public string lecturer { get; set; }
        public string lecturer_email { get; set; }
        public string discipline { get; set; }
        public string auditorium { get; set; }
        public string date_start { get; set; }
        public string date_end { get; set; }
        public List<StreamLink> stream_links { get; set; }
    }

    public class StreamLink
    {
        public string link { get; set; }
    }
}
