
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
    
public partial class AdditionalServices
{

    public int IDAdditionalServices { get; set; }

    public string Name { get; set; }

    public Nullable<int> Cost { get; set; }

    public Nullable<int> IDOrder { get; set; }



    public virtual Order Order { get; set; }

}

}
