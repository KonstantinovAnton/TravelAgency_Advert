//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TravelAgency
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tour()
        {
            this.Sale = new HashSet<Sale>();
        }
    
        public int id_tour { get; set; }
        public string tour_name { get; set; }
        public decimal price { get; set; }
        public System.DateTime departure_date { get; set; }
        public int departure_city_id { get; set; }
        public System.DateTime return_date { get; set; }
        public int return_city_id { get; set; }
        public int days_amount { get; set; }
        public int id_country { get; set; }
        public int id_tour_type { get; set; }
        public int id_nutrition { get; set; }
        public int id_hotel { get; set; }
        public string tour_img { get; set; }
    
        public virtual City City { get; set; }
        public virtual City City1 { get; set; }
        public virtual Country Country { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual Nutrition Nutrition { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sale { get; set; }
        public virtual Tour_Type Tour_Type { get; set; }
    }
}
