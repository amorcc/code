using cc.basemodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.model
{
    [Serializable]
    public class ProductCollectInfo :BaseModel
    {
        public string UserSN { get; set; }
        public int ProId { get; set; }
    }
}
