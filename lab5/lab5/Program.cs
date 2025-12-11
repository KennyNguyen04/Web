using Microsoft.EntityFrameworkCore;
using TodoApi; // Đảm bảo namespace này khớp với namespace trong Todo.cs và TodoDb.cs

var builder = WebApplication.CreateBuilder(args);

// --- PHẦN CẤU HÌNH DỊCH VỤ (SERVICES) ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Đăng ký Database Context (In-Memory)
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));

var app = builder.Build();

// --- PHẦN CẤU HÌNH PIPELINE (MIDDLEWARE) ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- PHẦN ĐỊNH NGHĨA API ENDPOINTS (CRUD) ---

// 0. API Test
app.MapGet("/", () => "Hello World!");

// 1. GET: Lấy danh sách tất cả công việc
app.MapGet("/todoitems", async (TodoDb db) =>
    await db.Todos.ToListAsync());

// 2. GET: Lấy danh sách các công việc đã hoàn thành
app.MapGet("/todoitems/complete", async (TodoDb db) =>
    await db.Todos.Where(t => t.IsComplete).ToListAsync());

// 3. GET: Lấy một công việc cụ thể theo ID
app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(todo)
            : Results.NotFound());

// 4. POST: Tạo mới một công việc
app.MapPost("/todoitems", async (Todo todo, TodoDb db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{todo.Id}", todo);
});

// 5. PUT: Cập nhật một công việc
app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

// 6. DELETE: Xóa một công việc
app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

// Chạy ứng dụng
app.Run();