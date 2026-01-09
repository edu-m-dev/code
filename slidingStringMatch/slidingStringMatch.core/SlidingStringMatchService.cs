namespace SlidingStringMatch.Core;

public static class SlidingStringMatchService
{
    public static int GetSlidingMatchMaxLength(string s, string p)
    {
        if (s is null || p is null) return 0;
        if (string.IsNullOrWhiteSpace(s) || string.IsNullOrWhiteSpace(p)) return 0;

        var maxLength = Math.Max(s.Length, p.Length);
        return
            Enumerable.Range(1, maxLength)
            .Select(i =>
            {
                // get last i characters of s using ranges
                var sSub = s.Length >= i ? s[^i..] : s;
                // get first i characters of p using ranges
                var pSub = p.Length >= i ? p[..i] : p;
                return sSub == pSub ? sSub.Length : 0;
            })
            .Max();
    }
}
