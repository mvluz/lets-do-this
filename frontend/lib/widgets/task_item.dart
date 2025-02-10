import 'package:flutter/material.dart';

class TaskItem extends StatelessWidget {
  final String task;
  final Function() onDelete;

  const TaskItem({super.key, required this.task, required this.onDelete});

  @override
  Widget build(BuildContext context) {
    return ListTile(
      title: Text(task),
      trailing: IconButton(
        icon: const Icon(Icons.delete),
        onPressed: onDelete,
      ),
    );
  }
}
