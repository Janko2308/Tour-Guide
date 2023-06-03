namespace Tour_Planner {
    public enum ThemeType {
        RedBlack,
        DeepDark,
        SoftDark,
    }

    public static class ThemeTypeExtension {
        public static string GetName(this ThemeType type) {
            switch (type) {
                case ThemeType.RedBlack:
                    return "RedBlackTheme";
                case ThemeType.DeepDark:
                    return "DeepDark";
                case ThemeType.SoftDark:
                    return "SoftDark";
                default:
                    return null;
            }
        }
    }
}