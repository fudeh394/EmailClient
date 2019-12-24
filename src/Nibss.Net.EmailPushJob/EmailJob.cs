using Quartz;
using System;


namespace EmailPushJob
{
    class EmailJob : IStatefulJob
    {
        #region IJob Members
        Util taskUtil = null;
        public void Execute(JobExecutionContext context)
        {
            try
            {
                taskUtil = new Util(context);
                taskUtil.Logger.InfoFormat("<<<Email Job Started>>>");
                new TaskImpl(taskUtil).DoJob();
                taskUtil.Logger.InfoFormat("<<<Email Job Ended>>>");
            }
            catch (Exception ex)
            {
                taskUtil.Logger.Error("Email Job", ex);
            }
        }


        #endregion
    }
}
