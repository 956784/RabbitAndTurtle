using System;
using System.Threading;

class AnimalThread
{
    private readonly string name;           // Имя животного
    private int distanceCovered;             // Пройденное расстояние
    private readonly Random random;          // Генератор случайных чисел

    // Конструктор для инициализации имени и переменных
    public AnimalThread(string name)
    {
        this.name = name;
        this.distanceCovered = 0;
        this.random = new Random();
    }

    // Метод, выполняющий основную логику бега животного
    public void Run()
    {
        while (distanceCovered < 100)
        {
            int step = random.Next(1, 10); // шаг от 1 до 9 метров
            distanceCovered += step;

            Console.WriteLine($"{name} пробежал {distanceCovered} м");

            // Изменение приоритета потока, если животное отстает
            AdjustPriority();

            // Имитация времени, необходимого для преодоления расстояния
            Thread.Sleep(100);
        }

        Console.WriteLine($"{name} достиг 100 м");
    }

    // Метод для изменения приоритета потока
    private void AdjustPriority()
    {
        if (distanceCovered < 50)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest; // Увеличить приоритет, если отстал
        }
        else
        {
            Thread.CurrentThread.Priority = ThreadPriority.Normal; // Снизить приоритет
        }
    }
}

class RabbitAndTurtle
{
    public static void Main()
    {
        // Создаем объекты животных
        AnimalThread rabbit = new AnimalThread("Кролик");
        AnimalThread turtle = new AnimalThread("Черепаха");

        // Создаем потоки для каждого животного
        Thread rabbitThread = new Thread(rabbit.Run);
        Thread turtleThread = new Thread(turtle.Run);

        // Запускаем потоки
        rabbitThread.Start();
        turtleThread.Start();

        // Ожидаем завершения потоков
        rabbitThread.Join();
        turtleThread.Join();
    }
}