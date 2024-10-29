//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlowerStore
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Order = new HashSet<Order>();
        }
    
        public int IDProduct { get; set; }
        public string NameProduct { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> IDTypeProduct { get; set; }
        public Nullable<int> AmountProduct { get; set; }
        public string DescriptionProduct { get; set; }
        public Nullable<int> IDSupplier { get; set; }
        public Nullable<int> ArticulProduct { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual TypeProduct TypeProduct { get; set; }
    }
}
