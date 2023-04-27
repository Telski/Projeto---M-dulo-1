using System;
using LabSchool.Enums;

namespace LabSchool.Extensions
{
    public static class EnumExtensions
    {
        public static string ToDisplayName(this EnumSituacaoMatricula situacao)
        {
            return Enum.GetName(typeof(EnumSituacaoMatricula), situacao);
        }
    }
}
