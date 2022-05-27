public struct ModelMessage
{
    public string UserId;
    public string FirstName;
    public string MessageText;
    public string UpdateId;
    public string MessageId;
  
    
    public int tag;

    public override string ToString()
    {
        return $"{MessageText}";
    }
}