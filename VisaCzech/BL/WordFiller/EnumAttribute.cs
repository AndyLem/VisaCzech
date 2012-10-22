namespace VisaCzech.BL.WordFiller
{
    public class EnumAttribute : StringAttribute
    {
        public int EnumValues;

        public EnumAttribute()
        {
            EnumValues = 0;
        }
    }
}
