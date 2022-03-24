using System;
using Timelog.Entities;

namespace Timelog.Services.Entities
{

    public class UserActivity
    {
        private UserActivityModel model;        


        public long Id
        {
            get
            {
                return model.Id;
            }
            set
            {
                model.Id = value;
            }
        }
        public string UniqId
        {
            get
            {
                return model.UniqId.ToString();
            }
        }
        public DateTime StartTime
        {
            get
            {
                return model.StartTime;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return model.EndTime;
            }
        }

        public string Title 
        {
            get 
            {
                return model.Title;
            }
            set 
            {
                model.Title = value;
            } 
        }

        public string Comment
        {
            get
            {
                return model.Comment;
            }
            set
            {
                model.Comment = value;
            }
        }

        public Guid UserUniqId
        {
            get
            {
                return model.UserUniqId;
            }
            set
            {
                model.UserUniqId = value;
            }
        }
             

        public long ProjectId
        {
            get { return model.ProjectId; }

        }       



        public UserActivity()
        {
            model = new UserActivityModel();
        }

        public UserActivity(UserActivityModel theModel)
        {
            model = theModel;
        }

        public static implicit operator UserActivity(UserActivityModel theModel)
        {
            if (theModel != null) return new UserActivity(theModel);
            else return null;
        }
        
        public static implicit operator UserActivityModel(UserActivity activity)
        {
            return activity?.model;
        }

        public void Start()
        {
            Start(DateTime.Now);
        }
        public void Start(DateTime customStart)
        {
            if (model.Status == UserActivityModel.ActivityStatus.Draft)
            {
                model.Status = UserActivityModel.ActivityStatus.Started;
                model.StartTime = customStart;                
            }
        }
        
        public void Stop()
        {
            Stop(DateTime.Now);
        }
       
        public void Stop(DateTime customEnd)
        {
            if (model.Status == UserActivityModel.ActivityStatus.Started)
            {
                model.Status = UserActivityModel.ActivityStatus.Complite;
                model.EndTime = customEnd;               
            }
        }
            

        public void SetProject(long projectId)
        {
            this.model.ProjectId = projectId;
        }
        public void SetProject(Project project)
        {
            if (project != null)
            {
                this.model.ProjectId = project.Id;  
            }
            else
            {
                throw new Exception("project is null");
            }
        }

        public void SetType(long typeId)
        {
            this.model.ActivityTypeId = typeId;
        }
        public void SetType(ActivityType type)
        {
            if (type != null)
            {
                this.model.ActivityTypeId = type.Id;
            }
            else
            {
                throw new Exception("type is null");
            }
        }
        

        public bool IsDraft()
        {
            return model.Status == UserActivityModel.ActivityStatus.Draft;
        }

        public bool IsStarted()
        {
            return model.Status == UserActivityModel.ActivityStatus.Started;
        }

        public bool IsComplite()
        {
            return model.Status == UserActivityModel.ActivityStatus.Complite;
        }

    }

}  // end of namespace Timelog.Core

