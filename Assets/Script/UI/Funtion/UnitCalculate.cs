
using UnityEngine;
public class UnitCalculate
{
    private static UnitCalculate instance;
    
    private UnitCalculate()
    {

    }
    public static UnitCalculate GetInstance()
    {
        if(UnitCalculate.instance == null)
            UnitCalculate.instance = new UnitCalculate();
        return UnitCalculate.instance;
    }

    public string Calculate(int Value)
    {
        string TranslateValue = "";
        TranslateValue = Value.ToString();
        if(Value >= 1000)
        {
            float TempValue;
            TempValue = Mathf.Floor(((float)Value / 1000.0f) * 100.0f) / 100.0f;
            TranslateValue = TempValue.ToString() + "k";
            if((Mathf.Floor(((float)Value / 1000.0f) * 100.0f) / 100.0f) % 1 == 0)
                TranslateValue = TempValue.ToString() + ".00k";
            if(Value >= 10000)
            {
                TempValue = Mathf.Floor(((float)Value / 1000.0f) * 10.0f) / 10.0f;
                TranslateValue = TempValue.ToString() + "k";
                if((Mathf.Floor(((float)Value / 1000.0f) * 10.0f) / 10.0f) % 1 == 0)
                    TranslateValue = TempValue.ToString() + ".0k";
                if(Value >= 100000)
                {
                    TempValue = Mathf.Floor((float)Value / 1000.0f);
                    TranslateValue = TempValue.ToString() + "k";
                    if(Value >= 1000000)
                    {
                        TempValue = Mathf.Floor(((float)Value / 1000000.0f) * 100.0f) / 100.0f;
                        TranslateValue = TempValue.ToString() + "m";
                        if((Mathf.Floor(((float)Value / 100000.0f) * 100.0f) / 100.0f) % 1 == 0)
                            TranslateValue = TempValue.ToString() + ".00m";
                        if(Value >= 10000000)
                        {
                            TempValue = Mathf.Floor(((float)Value / 1000000.0f) * 10.0f) / 10.0f;
                            TranslateValue = TempValue.ToString() + "m";
                            if((Mathf.Floor(((float)Value / 100000.0f) * 100.0f) / 100.0f) % 1 == 0)
                                TranslateValue = TempValue.ToString() + ".0m";
                            if(Value >= 100000000)
                            {
                                TempValue = Mathf.Floor((float)Value / 1000000.0f);
                                TranslateValue = TempValue.ToString() + "m";
                            }
                        }
                    }
                }
            }
        }
        return TranslateValue;
    }
}
