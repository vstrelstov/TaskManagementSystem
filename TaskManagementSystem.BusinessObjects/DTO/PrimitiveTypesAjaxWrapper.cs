namespace TaskManagementSystem.BusinessObjects.DTO
{
    /// <summary>
    /// Таким образом обходились проблемы с передачей простых типов типа number
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PrimitiveTypesAjaxWrapper<T>
    {
        public T Value { get; set; }
    }
}
