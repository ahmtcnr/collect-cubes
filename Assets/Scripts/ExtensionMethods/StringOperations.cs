using System;

public static class StringOperations
{
    public static string ConvertToTimer(this float currentSecond)
    {
        var ss = Convert.ToInt32(currentSecond % 60).ToString("00");
        var mm = (Math.Floor(currentSecond / 60) % 60).ToString("00");
        return $"{mm}:{ss}";
    }
}