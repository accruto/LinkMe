function ClearInputOfDefaultValue(input, initialValue)
{
    if($F(input) == initialValue)
    {
        $(input).value = '';
    }
}
