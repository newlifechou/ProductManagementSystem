//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBTransferFromOldToNew
{
    using System;
    using System.Collections.Generic;
    
    public partial class PMSOrders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PMSOrders()
        {
            this.PMSPlanVHPs = new HashSet<PMSPlanVHPs>();
        }
    
        public System.Guid ID { get; set; }
        public string CustomerName { get; set; }
        public string PO { get; set; }
        public string CompositionStandard { get; set; }
        public string CompositionOriginal { get; set; }
        public string ProductType { get; set; }
        public string Purity { get; set; }
        public double Quantity { get; set; }
        public string QuantityUnit { get; set; }
        public string Dimension { get; set; }
        public string DimensionDetails { get; set; }
        public string SampleNeed { get; set; }
        public System.DateTime DeadLine { get; set; }
        public string MinimumAcceptDefect { get; set; }
        public string Remark { get; set; }
        public string Priority { get; set; }
        public string State { get; set; }
        public string StateRemark { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public string PolicyType { get; set; }
        public string PMINumber { get; set; }
        public string CompositionAbbr { get; set; }
        public System.DateTime ReviewTime { get; set; }
        public string Reviewer { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PMSPlanVHPs> PMSPlanVHPs { get; set; }
    }
}
