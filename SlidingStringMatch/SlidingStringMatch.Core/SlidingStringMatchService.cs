namespace SlidingStringMatch.Core;

public static class SlidingStringMatchService
{
    // Calculates the maximum length of a contiguous matching substring
    // between two strings s and p, considering all possible sliding alignments.
    //
    // Example:
    //   s = "abcdefg"
    //   p = "cdezz"
    //   The best overlap is "cde" → returns 3
    //
    // The algorithm works by sliding p across s from far left to far right,
    // checking the overlapping region at each position, and tracking the
    // longest run of consecutive matching characters.

    public static int GetSlidingMatchMaxLength(string s, string p)
    {
        if (s is null || p is null) return 0;
        if (string.IsNullOrWhiteSpace(s) || string.IsNullOrWhiteSpace(p)) return 0;

        // This will store the best (maximum) contiguous match found.
        int max = 0;

        // We slide p across s by shifting p's starting index relative to s.
        //
        // offset = -p.Length + 1 means p starts completely to the left of s,
        // with only its last character touching s's first character.
        //
        // offset increases until p is completely to the right of s.
        //
        // Example:
        //   s:  abcdefg
        //   p:      xyz
        //
        // offset = -2  -1   0   1   2   3   4   5   6
        //
        // This ensures we test *every possible alignment*.
        for (int offset = -p.Length + 1; offset < s.Length; offset++)
        {
            // Tracks the current run of consecutive matching characters
            // for this specific alignment.
            int current = 0;

            // Loop through all characters of p.
            for (int i = 0; i < p.Length; i++)
            {
                // si = the corresponding index in s for p[i]
                int si = offset + i;

                // If si is outside the bounds of s, then p[i] does not overlap s.
                // We skip it and continue.
                if (si < 0 || si >= s.Length)
                    continue;

                // If characters match, we extend the current run.
                if (s[si] == p[i])
                {
                    current++;

                    // Update global maximum if this run is the best so far.
                    if (current > max)
                        max = current;
                }
                else
                {
                    // Characters differ → reset the contiguous run counter.
                    current = 0;
                }
            }
        }

        return max;
    }
}
