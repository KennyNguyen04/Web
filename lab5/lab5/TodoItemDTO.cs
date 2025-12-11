using TodoApi;

namespace TodoApi
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }

        public TodoItemDTO() { }

        // Constructor giúp chuyển đổi nhanh từ Todo sang DTO
        public TodoItemDTO(Todo todoItem) =>
            (Id, Name, IsComplete) = (todoItem.Id, todoItem.Name, todoItem.IsComplete);
    }
}