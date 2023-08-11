namespace Smartwyre.DeveloperTest.Types;

[System.Flags]
public enum IncentiveType
{
    FixedRate = 1 << 0,
    AmountPerUom = 1 << 1,
    FixedCashAmount = 1 << 2
}
