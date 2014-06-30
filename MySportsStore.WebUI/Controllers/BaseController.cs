using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MySportsStore.WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected IList<IDisposable> DisposableObjects { get; private set; }

        public BaseController()
        {
            this.DisposableObjects = new List<IDisposable>();
        }

        protected void AddDisposableObject(object obj)
        {
            IDisposable disposable = obj as IDisposable;
            if (disposable != null)
            {
                this.DisposableObjects.Add(disposable);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (IDisposable obj in this.DisposableObjects)
                {
                    if (null != obj)
                    {
                        obj.Dispose();
                    }
                }
            }
            base.Dispose(disposing);
        }
    }
}
