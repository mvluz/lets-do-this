class Task {
  String id;
  String name;
  String details;
  bool completed;

  // Removi o 'final' de 'name' e 'details' para permitir a alteração após a criação
  Task({
    required this.id,
    required this.name,
    required this.details,
    this.completed = false,
  });

  // Modificando a factory para criar um id caso ele não seja fornecido
  factory Task.fromJson(Map<String, dynamic> json) {
    return Task(
      id: json['_id'] ?? '', // Aqui, ajustamos para '_id', que pode vir do seu banco de dados
      name: json['name'],
      details: json['details'],
      completed: json['completed'] ?? false,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'details': details,
      'completed': completed,
    };
  }
}
