import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/task.dart';

class TaskService {
  static const String apiUrl = 'http://localhost:5000/api/tasks'; // Substitua pela URL da sua API

  static Future<List<Task>> getTasks() async {
    final response = await http.get(Uri.parse(apiUrl));

    if (response.statusCode == 200) {
      List<dynamic> data = json.decode(response.body);
      return data.map((task) => Task.fromJson(task)).toList();
    } else {
      throw Exception('Falha ao carregar tarefas');
    }
  }

  static Future<void> addTask(Task task) async {
    final response = await http.post(
      Uri.parse(apiUrl),
      headers: {'Content-Type': 'application/json'},
      body: json.encode({
        'name': task.name,
        'details': task.details,
        'completed': task.completed,
      }),
    );

    if (response.statusCode != 201) {
      throw Exception('Falha ao adicionar tarefa');
    }
  }

  static Future<void> updateTask(Task task) async {
    final response = await http.put(
      Uri.parse('$apiUrl/${task.id}'),
      headers: {'Content-Type': 'application/json'},
      body: json.encode({
        'name': task.name,
        'details': task.details,
        'completed': task.completed,
      }),
    );

    if (response.statusCode != 200) {
      throw Exception('Falha ao atualizar tarefa');
    }
  }
}
