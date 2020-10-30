namespace TaskManagementSystem.BusinessObjects.DTO
{
    /// <summary>
    /// Обёртка для передачи примитивных типов в контроллер через axios
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PrimitiveTypesAjaxWrapper<T>
    {
        public T Value { get; set; }
    }
}
