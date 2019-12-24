using Quartz;
using System;


namespace EmailPushJob
{
    class MaintenaceJob : IStatefulJob
    {
        #region IJob Members
        Util taskUtil = null;
        public void Execute(JobExecutionContext context)
        {
            try
            {
                taskUtil = new Util(context);
                taskUtil.Logger.InfoFormat("<<<Maintenance Job Started>>>");
                new TaskImpl(taskUtil).DoMaintenance();
                taskUtil.Logger.InfoFormat("<<<Maintenance Job Ended>>>");
            }
            catch (Exception ex)
            {
                taskUtil.Logger.Error("Email Job", ex);
            }
        }


        #endregion
    }
}
