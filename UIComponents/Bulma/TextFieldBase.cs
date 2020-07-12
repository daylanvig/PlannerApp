namespace UIComponents.Bulma
{
    public class TextFieldBase : InputFieldBase<string>
    {
        protected override bool TryParseValueFromString(string value, out string result, out string validationErrorMessage)
        {
            // Text inputs dont neeed to be converted
            result = value;
            validationErrorMessage = string.Empty;
            return true;
        }
    }
}
