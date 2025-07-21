using TMPro;
using UnityEngine;

namespace SGSTools.Components
{
    // TODO: Extend for higher frame-rates(100+ FPS).
    public class FPSCounter : MonoBehaviour
    {
        private readonly string[] _numbers = new string[]
        {
            "00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
            "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
            "20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
            "30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
            "40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
            "50", "51", "52", "53", "54", "55", "56", "57", "58", "59",
            "60", "61", "62", "63", "64", "65", "66", "67", "68", "69",
            "70", "71", "72", "73", "74", "75", "76", "77", "78", "79",
            "80", "81", "82", "83", "84", "85", "86", "87", "88", "89",
            "90", "91", "92", "93", "94", "95", "96", "97", "98", "99",
            "100", "101", "102", "103", "104", "105", "106", "107", "108", "109",
            "110", "111", "112", "113", "114", "115", "116", "117", "118", "119",
            "120", "121", "122", "123", "124", "125", "126", "127", "128", "129",
            "130", "131", "132", "133", "134", "135", "136", "137", "138", "139",
            "140", "141", "142", "143", "144", "145", "146", "147", "148", "149",
            "150", "151", "152", "153", "154", "155", "156", "157", "158", "159",
            "160", "161", "162", "163", "164", "165", "166", "167", "168", "169",
            "170", "171", "172", "173", "174", "175", "176", "177", "178", "179",
            "180", "181", "182", "183", "184", "185", "186", "187", "188", "189",
            "190", "191", "192", "193", "194", "195", "196", "197", "198", "199",
        };

        public TMP_Text TextMax;
        public TMP_Text TextAverage;
        public TMP_Text TextMin;

        private readonly Color _orange = new Color(1f, 0.65f, 0f);
        private float[] _framesBuffer = new float[30];
        private int _nextIndex;

        private void Awake()
        {
            for (int i = 0; i < _framesBuffer.Length; i++)
            {
                _framesBuffer[i] = 0;
            }
            _nextIndex = 0;
        }

        void Update()
        {
            _framesBuffer[_nextIndex] = 1f / Time.unscaledDeltaTime;
            _nextIndex = ++_nextIndex % _framesBuffer.Length;
            UpdateText(TextMax, GetMaxFPS());
            UpdateText(TextAverage, GetAverageFPS());
            UpdateText(TextMin, GetMinFPS());
        }

        private void UpdateText(TMP_Text text, int fps)
        {
            text.text = _numbers[Mathf.Clamp(fps, 0, 99)];
            Color color;
            if (fps < 30)
            {
                color = Color.red;
            }
            else if (fps < 40)
            {
                color = _orange;
            }
            else if (fps < 50)
            {
                color = Color.yellow;
            }
            else if (fps < 90)
            {
                color = Color.green;
            }
            else
            {
                color = Color.cyan;
            }
            text.color = color;
        }

        private int GetAverageFPS()
        {
            float total = 0f;
            for (int i = 0; i < _framesBuffer.Length; i++)
            {
                total += _framesBuffer[i];
            }
            return Mathf.RoundToInt(total / _framesBuffer.Length);
        }

        private int GetMaxFPS()
        {
            float max = 0f;
            for (int i = 0; i < _framesBuffer.Length; i++)
            {
                if (_framesBuffer[i] > max)
                {
                    max = _framesBuffer[i];
                }
            }
            return Mathf.RoundToInt(max);
        }

        private int GetMinFPS()
        {
            float min = 99f;
            for (int i = 0; i < _framesBuffer.Length; i++)
            {
                if (_framesBuffer[i] < min)
                {
                    min = _framesBuffer[i];
                }
            }
            return Mathf.RoundToInt(min);
        }
    }
}