using System;
using System.Linq;
using System.Collections.Generic;

namespace Livis.Common.Tools
{

    public static class VariantTool
    {
        public static string[] GenerateVariantOptions(List<string[]> variantOptions)
        {
            if (variantOptions.Count == 0)
                return new string[] { };

            if (variantOptions.Count > 1)
            {
                var index = 0;
                var result = new List<string>();
                while (index < variantOptions.Count)
                {
                    if (result.Count == 0)
                    {
                        result = variantOptions[index].SelectMany(first => variantOptions[index + 1], (f, s) => $"{f}-{s}").ToList();
                        index += 2;
                    }
                    else
                    {
                        result = result.SelectMany(first => variantOptions[index], (f, s) => $"{f}-{s}").ToList();
                        index++;
                    }
                }
                return result.ToArray();
            }
            return variantOptions.First();
        }
    }
}
