import 'package:flutter/material.dart';
import '../models/task.dart';
import '../services/task_service.dart';

class TaskListScreen extends StatefulWidget {
  const TaskListScreen({super.key});

  @override
  _TaskListScreenState createState() => _TaskListScreenState();
}

class _TaskListScreenState extends State<TaskListScreen> {
  final TextEditingController taskController = TextEditingController();
  final TextEditingController taskDetailsController = TextEditingController();
  List<Task> tasks = [];

  @override
  void initState() {
    super.initState();
    _loadTasks();
  }

  // Carregar tarefas
  void _loadTasks() async {
    tasks = await TaskService.getTasks();
    setState(() {});
  }

  // Adicionar nova tarefa
  void _addTask() async {
    if (taskController.text.isEmpty) return;

    // Gerar ID automaticamente, caso não tenha sido passado
    String newTaskId = DateTime.now().millisecondsSinceEpoch.toString();

    Task newTask = Task(
      id: newTaskId, // Gerando um ID temporário
      name: taskController.text,
      details: taskDetailsController.text,
      completed: false,
    );

    await TaskService.addTask(newTask);
    _loadTasks(); // Recarregar a lista de tarefas
    taskController.clear();
    taskDetailsController.clear();

    // Voltar para a tela de lista de tarefas após adicionar
    Navigator.pop(context);
  }

  // Marcar tarefa como concluída
  void _toggleComplete(Task task) async {
    task.completed = !task.completed;
    await TaskService.updateTask(task);
    _loadTasks();
  }

  // Compartilhar tarefa
  void _shareTask(Task task) async {
    // Adicionar código para enviar e-mail de compartilhamento ou outra forma de compartilhamento
    ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text('Tarefa compartilhada: ${task.name}')));
  }

  // Editar tarefa
  void _editTask(Task task) async {
    taskController.text = task.name;
    taskDetailsController.text = task.details;

    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Editar Tarefa'),
        content: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            TextField(
              controller: taskController,
              decoration: const InputDecoration(labelText: 'Nome da Tarefa'),
            ),
            TextField(
              controller: taskDetailsController,
              decoration: const InputDecoration(labelText: 'Detalhes da Tarefa'),
            ),
          ],
        ),
        actions: [
          TextButton(
            onPressed: () async {
              // Atualizando os valores de 'name' e 'details'
              task.name = taskController.text;
              task.details = taskDetailsController.text;
              await TaskService.updateTask(task);
              Navigator.of(context).pop();
              _loadTasks();
            },
            child: const Text('Salvar'),
          ),
          TextButton(
            onPressed: () => Navigator.of(context).pop(),
            child: const Text('Cancelar'),
          ),
        ],
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Lista de Tarefas')),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          children: [
            TextField(
              controller: taskController,
              decoration: const InputDecoration(labelText: 'Nome da Tarefa'),
            ),
            TextField(
              controller: taskDetailsController,
              decoration: const InputDecoration(labelText: 'Detalhes da Tarefa'),
            ),
            ElevatedButton(
              onPressed: _addTask,
              child: const Text('Adicionar Tarefa'),
            ),
            Expanded(
              child: ListView.builder(
                itemCount: tasks.length,
                itemBuilder: (context, index) {
                  final task = tasks[index];
                  return ListTile(
                    title: Text(task.name),
                    subtitle: Text(task.details),
                    trailing: Wrap(
                      spacing: 10,
                      children: [
                        IconButton(
                          icon: Icon(task.completed ? Icons.check_box : Icons.check_box_outline_blank),
                          onPressed: () => _toggleComplete(task),
                        ),
                        IconButton(
                          icon: const Icon(Icons.edit),
                          onPressed: () => _editTask(task),
                        ),
                        IconButton(
                          icon: const Icon(Icons.share),
                          onPressed: () => _shareTask(task),
                        ),
                      ],
                    ),
                  );
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}
