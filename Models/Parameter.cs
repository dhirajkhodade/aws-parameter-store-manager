namespace aws_parameter_store_manager.Model
{
    public class Parameter
    {
        public string Name { get; set; }    
        public string Value { get; set; }
        public string Type { get; set; }    
        public string DataType { get; set; }
        public bool Overwrite { get; set; }
    }
}