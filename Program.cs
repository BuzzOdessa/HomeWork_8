// See https://aka.ms/new-console-template for more information



int count = 100;

Console.WriteLine($"Тест скорости  синхронного создания {count} файлов: ");
var watch = new System.Diagnostics.Stopwatch();
watch.Start();

for (int i = 0; i < count; i++)
{
    var fileName = i.ToString();
    File.AppendAllText(i.ToString(), $"{i}\n");
    File.Delete(fileName);
}
watch.Stop();
Console.WriteLine(@$"Время исполнения:  {watch.ElapsedMilliseconds} ms");

//--------------------------------------------------------------------------------
Console.WriteLine("--------------------------------------------------------------------------------");

Console.WriteLine($"Тест скорости  Асинхронного создания {count} файлов: ");
watch.Reset();
watch.Start();

var tasks = new List<Task>();
for (int i = 0; i < count; i++)
{
    tasks.Add(WriteToFile(i));
}
//await Task.WhenAll(tasks.ToArray());
Task.WaitAll(tasks.ToArray());
watch.Stop();
Console.WriteLine(@$"Время исполнения:  {watch.ElapsedMilliseconds} ms");
Console.ReadKey();

async Task WriteToFile(int fileNum)
{
    var fileName = fileNum.ToString();
    await Task.Run(() =>
    {

        // await File.AppendAllTextAsync(fileNum.ToString(), $"{fileNum}\n");
        File.AppendAllText(fileNum.ToString(), $"{fileNum}\n");
        File.Delete(fileName);
    })
    ;
}
