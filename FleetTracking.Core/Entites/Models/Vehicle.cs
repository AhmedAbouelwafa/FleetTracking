using FleetTracking.Core.Entites.Base;
using FleetTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Core.Entites.Models
{
    public class Vehicle : Entity<int>
    {
        // 1️⃣ البيانات الأساسية (Static Data)
        public string PlateNumber { get; set; }
        // رقم اللوحة: "أ ب ج 1234"
        // ده ثابت، مش بيتغير إلا لو غيرت اللوحة

        public string DriverName { get; set; }
        // اسم السواق: "محمد أحمد"
        // ممكن يتغير لو السيارة اتنقلت لسواق تاني


        // 2️⃣ آخر موقع معروف (Real-time Cache)
        public double? LastLatitude { get; set; }
        // آخر خط عرض: 30.0444 (مثلاً القاهرة)
        // الـ ? معناها nullable → ممكن يكون null لو السيارة لسه مبعتتش موقع

        public double? LastLongitude { get; set; }
        // آخر خط طول: 31.2357

        public double? LastSpeed { get; set; }
        // آخر سرعة: 85.5 km/h
        // nullable عشان لو السيارة واقفة أو offline

        public double? LastHeading { get; set; }
        // اتجاه الحركة: 0-360 درجة
        // 0° = شمال، 90° = شرق، 180° = جنوب، 270° = غرب
        // مهم عشان ترسم السهم على الخريطة صح

        public DateTime? LastUpdatedAt { get; set; }
        // ⚠️ آخر مرة السيارة بعتت موقع
        // مختلف عن Entity.UpdatedAt (شرح تحت ⬇️)


        // 3️⃣ حالة السيارة (Computed Status)
        public VehicleStatus Status { get; set; }
        // Moving: السرعة > 5 km/h
        // Stopped: السرعة = 0 من أقل من 5 دقائق
        // Idle: السرعة = 0 من أكتر من 5 دقائق
        // Offline: مفيش تحديث من أكتر من 10 دقائق
    }

}
