using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Interface;
using TaskManager.Core.Models;
using TaskManager.Data.Interface;
using TaskManager.Data.Provider.Sql.Repository;
using System.Linq;

namespace TaskManager.Business
{
    //La couche Business contient toute la logique
    public class TaskManagerDomain : ITaskManagerDomain
    {
        private readonly IBoardRepository boardRepository;
        private readonly ISectionRepository sectionRepository;
        private readonly ITaskRepository taskRepository;

        public TaskManagerDomain(
            IBoardRepository boardRepository,
            ISectionRepository sectionRepository,
            ITaskRepository taskRepository)
        {
            this.boardRepository = boardRepository;
            this.sectionRepository = sectionRepository;
            this.taskRepository = taskRepository;
        }

        //Récupère tout les tableaux
        public async Task<IEnumerable<BoardDTO>> GetAllBoards() => await boardRepository.GetAll();

        //Récupère un tableau via son id
        public async Task<BoardDTO> GetBoardById(int id) => await boardRepository.GetById(id);

        //Crée un tableau non verrouillé avec ses trois sections (Todo, Doing, Done)
        public async Task<BoardDTO> CreateBoard(BoardDTO board)
        {
            board.IsLocked = false;
            var boardDb = await boardRepository.Create(board);

            await sectionRepository.Create(new SectionDTO()
            {
                Name = "Todo",
                BoardId = boardDb.Id
            });
            await sectionRepository.Create(new SectionDTO()
            {
                Name = "Doing",
                BoardId = boardDb.Id
            });
            await sectionRepository.Create(new SectionDTO()
            {
                Name = "Done",
                BoardId = boardDb.Id
            });

            return boardDb;
        }

        //Met à jour le tableau
        public async Task<int> UpdateBoard(BoardDTO board)
        {
            return await boardRepository.Update(board);
        }         

        //Récupère toute les tâche
        public async Task<IEnumerable<TaskDTO>> GetAllTasks() => await taskRepository.GetAll();

        //Récupère une tâche via son id
        public async Task<TaskDTO> GetTaskById(int id) => await taskRepository.GetById(id);

        //Crée une tâche grâce à l'id du tableau passé en paramètre et on l'enregistre directement dans la section "Todo"
        public async Task<TaskDTO> CreateTask(int idBoard, TaskDTO task)
        {
            var board = await boardRepository.GetById(idBoard);
            if (board.IsLocked)
            {
                return null;
            }

            //Récupère toute les sections, ensuite on récupère la section "Todo" dans les sections qui correspondent au tableau
            var sections = await sectionRepository.GetAll();
            var sectionTodo = sections.Where(id => id.BoardId == idBoard).FirstOrDefault(n => n.Name == "Todo");
            if (sectionTodo == null)
            {
                return null;
            }

            //crée la tâche avec l'id de la section
            task.SectionId = sectionTodo.Id;
            return await taskRepository.Create(task);
        }

        //met à jour une tâche
        public async Task<int> UpdateTask(TaskDTO task)
        {
            var board = await VerifyIsLocked(task.Id);

            if (board.IsLocked)
            {
                return 0;
            }

            return await taskRepository.Update(task);
        }

        //Permet de changer une tâche de section
        public async Task<int> ChanseSectionTask(int id, TaskDTO task)
        {
            var boardIsLock = await VerifyIsLocked(task.Id);

            if (boardIsLock.IsLocked)
            {
                return 0;
            }

            //Récupère la tache à modifier et la section dans laquelle elle se trouve
            //Récupère la section dans laquelle on veux enregistrer la tâche
            var taskOrigine = await taskRepository.GetById(id);
            var sectionOrigine = await sectionRepository.GetById(taskOrigine.SectionId);
            var sectionTask = await sectionRepository.GetById(task.SectionId);

            //On vérifie que les deux sections se trouve dans le même tableau
            if (sectionOrigine.BoardId == sectionTask.BoardId)
            {
                //Vérifie les deux cas dans lesquelles on ne peux pas changer de section
                if (((sectionOrigine.Name == "Todo") && (sectionTask.Name == "Done")) || ((sectionOrigine.Name == "Done") && (sectionTask.Name == "Todo")))
                {
                    return 0;
                }
                return await taskRepository.Update(task);
            }
            return 0;
        }

        //Supprime une tâche
        public async Task<int> DeleteTask(int id)
        {
            var boardIsLock = await VerifyIsLocked(id);

            if (boardIsLock.IsLocked)
            {
                return 0;
            }

            return await taskRepository.Delete(id);
        }

        //Récupère tout les sections
        public async Task<IEnumerable<SectionDTO>> GetAllSections()
        {
            return await sectionRepository.GetAll();
        }

        //Récupère une section via son id
        public async Task<SectionDTO> GetSectionById(int id)
        {
            return await sectionRepository.GetById(id);
        }

        //Vérifie via l'id d'une tâche si le tableau dans lequel il se trouve est vérrouillé ou non
        private async Task<BoardDTO> VerifyIsLocked(int id)
        {
            var task = await GetTaskById(id);
            var section = await sectionRepository.GetAll();
            var boardId = section.FirstOrDefault(i => i.Id == task.SectionId).BoardId;
            return await boardRepository.GetById(boardId);
        }
    }
}
