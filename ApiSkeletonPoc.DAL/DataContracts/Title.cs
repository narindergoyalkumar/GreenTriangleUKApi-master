using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class Title
    {
        public Title()
        {
            Individual = new HashSet<Individual>();
        }

        public int TitleId { get; set; }
        public string Title1 { get; set; }

        public virtual ICollection<Individual> Individual { get; set; }
    }
}
