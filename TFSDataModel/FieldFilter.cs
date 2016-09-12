using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    /// <summary>
    /// FieldFilter Class
    /// </summary>
    public class FieldFilter
    {
        public FieldFilter()
        {
            this.AssignedTo = new TFSResourceIdentity();
        }

        /// <summary>
        /// Gets or sets the type of the work item.
        /// </summary>
        /// <value>
        /// The type of the work item.
        /// </value>
        public short WorkItemType { get; set; }

        /// <summary>
        /// Gets or sets the iteration.
        /// </summary>
        /// <value>
        /// The iteration.
        /// </value>
        public string Iteration { get; set; }

        /// <summary>
        /// Gets or sets the release.
        /// </summary>
        /// <value>
        /// The release.
        /// </value>
        public string Release { get; set; }

        /// <summary>
        /// Gets or sets the assigned to.
        /// </summary>
        /// <value>
        /// The assigned to.
        /// </value>
        public TFSResourceIdentity AssignedTo { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public short State { get; set; }

        /// <summary>
        /// Gets or sets the date range.
        /// </summary>
        /// <value>
        /// The date range.
        /// </value>
        public Duration DateRange { get; set; }
    }
}
