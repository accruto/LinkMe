namespace LinkMe.Apps.Reports
{
    public enum StyleItem
    {
        ReportName,
        Title,
        SubTitle,
        NonBodyLine,
        FooterText,
        SummaryLabel,
        SummaryText,
        TableHeader,
        TableText,
        TableFooter,
        NotSoSmallTableText,
        SmallTableHeader,
        SmallTableText,
        SmallTableFooter,
    }

    public enum BorderLocation
    {
        Left,
        Right,
        Top,
        Bottom
    }

    public static class Styles
    {
        public static string FontColor(StyleItem item)
        {
            switch (item)
            {
                case StyleItem.ReportName:
                case StyleItem.SummaryText:
                case StyleItem.TableText:
                case StyleItem.NotSoSmallTableText:
                case StyleItem.SmallTableText:
                    return "Black";

                case StyleItem.Title:
                case StyleItem.SubTitle:
                case StyleItem.SummaryLabel:
                case StyleItem.TableHeader:
                case StyleItem.TableFooter:
                case StyleItem.SmallTableHeader:
                case StyleItem.SmallTableFooter:
                    return "MidnightBlue";

                case StyleItem.FooterText:
                    return "Gray";

                default:
                    return "Black";
            }
        }

        public static string FontFamily(StyleItem item)
        {
            switch (item)
            {
                case StyleItem.ReportName:
                case StyleItem.Title:
                case StyleItem.SubTitle:
                case StyleItem.FooterText:
                    return "Arial";

                case StyleItem.SummaryLabel:
                case StyleItem.SummaryText:
                case StyleItem.TableHeader:
                case StyleItem.TableFooter:
                case StyleItem.SmallTableHeader:
                case StyleItem.SmallTableFooter:
                case StyleItem.TableText:
                case StyleItem.NotSoSmallTableText:
                case StyleItem.SmallTableText:
                    return "Verdana";

                default:
                    return "Arial";
            }
        }

        public static string FontSize(StyleItem item)
        {
            switch (item)
            {
                case StyleItem.ReportName:
                    return "16pt";

                case StyleItem.Title:
                    return "18pt";

                case StyleItem.SubTitle:
                    return "12pt";

                case StyleItem.SummaryLabel:
                case StyleItem.SummaryText:
                    return "10pt";

                case StyleItem.TableHeader:
                case StyleItem.TableFooter:
                    return "9pt";

                case StyleItem.SmallTableHeader:
                case StyleItem.SmallTableFooter:
                    return "7pt";

                case StyleItem.FooterText:
                case StyleItem.TableText:
                    return "8pt";

                case StyleItem.NotSoSmallTableText:
                    return "7pt";

                case StyleItem.SmallTableText:
                    return "6pt";

                default:
                    return "12pt";
            }
        }

        public static string FontWeight(StyleItem item)
        {
            switch (item)
            {
                case StyleItem.ReportName:
                case StyleItem.Title:
                case StyleItem.SubTitle:
                case StyleItem.SummaryLabel:
                case StyleItem.TableHeader:
                case StyleItem.TableFooter:
                case StyleItem.SmallTableHeader:
                case StyleItem.SmallTableFooter:
                    return "Bold";

                case StyleItem.FooterText:
                case StyleItem.SummaryText:
                case StyleItem.TableText:
                case StyleItem.NotSoSmallTableText:
                case StyleItem.SmallTableText:
                    return "Normal";

                default:
                    return "Normal";
            }
        }

        public static string LineColor(StyleItem item)
        {
            switch (item)
            {
                case StyleItem.NonBodyLine:
                    return "DimGray";

                default:
                    return "Black";
            }
        }

        public static string LineStyle(StyleItem item)
        {
            switch (item)
            {
                case StyleItem.NonBodyLine:
                default:
                    return "Solid";
            }
        }

        public static string LineWidth(StyleItem item)
        {
            switch (item)
            {
                case StyleItem.NonBodyLine:
                default:
                    return "1pt";
            }
        }

        public static string BorderColor(StyleItem item, BorderLocation location)
        {
            switch (item)
            {
                default:
                    return "Black";
            }
        }

        public static string BorderStyle(StyleItem item, BorderLocation location)
        {
            switch (item)
            {
                case StyleItem.TableHeader:
                case StyleItem.SmallTableHeader:
                    switch (location)
                    {
                        case BorderLocation.Bottom:
                            return "Solid";

                        default:
                            return "None";
                    }

                case StyleItem.TableFooter:
                case StyleItem.SmallTableFooter:
                    switch (location)
                    {
                        case BorderLocation.Top:
                            return "Solid";

                        default:
                            return "None";
                    }


                default:
                    return "None";
            }
        }

        public static string BorderWidth(StyleItem item, BorderLocation location)
        {
            switch (item)
            {
                default:
                    return "1pt";
            }
        }
    }
}
