// -----------------------------------------------------------------------
// <copyright file="TemplateFactoryExtensions.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.AutoBuilder.Console.FluentConfiguration.ExtensionMethods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CuttingEdge.Conditions;
    using NCastor.AutoBuilder.Console.Services;

    /// <summary>
    /// This class contains extenssion methods to be used with the ITemplateFactory interface
    /// </summary>
    public static class TemplateFactoryExtensions
    {
        /// <summary>
        /// Searches the specified embedded file in the specified assembly
        /// </summary>
        /// <param name="templateFactory">The template factory.</param>
        /// <param name="embeddedFileName">Name of the embedded file.</param>
        /// <param name="resourceNamespace">The resource namespace.</param>
        /// <returns>
        /// Gets a reference to the parent ITemplateConfigurator object
        /// </returns>
        /// <remarks>
        /// This method calls the ITemplateFactory.FindEmbeddedResource method, its basically a wrapper
        /// </remarks>
        public static ITemplateConfigurator FindEmbeddedResource(this ITemplateFactory templateFactory, string embeddedFileName, string resourceNamespace)
        {
            Condition.Requires(templateFactory, "templateFactory").IsNotNull();

            /*
             * 
             * The justification for this method is just an idea of mine =), the thoghts that have influenced me are in
             * the following links:
             * 
             * (I highly recommend reading Misko Hevery posts)
             * 
             * http://www.loosecouplings.com/2011/01/how-to-write-testable-code-overview.html
             * http://misko.hevery.com/2008/09/30/to-new-or-not-to-new/
             * http://www.youtube.com/watch?v=wEhu57pih5w
             * 
             * Basically the idea is to identify two kind of objects in our code:
             * 
             * -Newables
             * -Injectables
             * 
             * --Injectable are objects that will expose dependencies in their constructors and these dependencies usually
             * will be resolved using a container. The important thing about injectable objects is that they can expose
             * dependencies ONLY to other injectable objects 
             * 
             * --Newables are objects that will also expose dependencies in their constructors but these dependencies
             * will be ONLY other newable objects
             * 
             * Now, Injectables can only require other injectables in their constructors and newables can only
             * require other newables but it turns out that we will be usually requiring services (injectables) in our
             * newable objects (for example Entities - DDD) but there is also a recomendation that a newable should not
             * hold references to injectable objects, so following these thoughts I choose the following design
             * 
             * **Passing the injectable object to a newable object at a method level**
             * 
             * This sounds freaking at first to be honest, just the idea to resolve the dependency tree for a single
             * service each time we invoke a method .... hell no
             * 
             * But in the other hand we would have a clearly separation between newables and injectable in our 
             * application and also we would have the ability to use services in our newable objects but the best thing
             * is that we will be creating testing-friendly objects
             * 
             * Think about this a little, when we expose all dependencies in constructors we have to have class level
             * members to hold the references until a method wants to use them, even if we only have one method that 
             * actually uses the dependency this might not sound so terrible for you at first...  
             * Let's see this from a tresting point of view, in a test, when we create the object, we
             * would have to specify the dependency even if we want to test a method that actually it is not using it
             * and if we abuse of null checks... well we will not be able to just pass a null reference to the constructor
             * and the test is going to become more and more complex
             * 
             * So exposing the dependencies at a method level seems more accurate to me but it requires much more effort
             * to accomplish
             * 
             * So I came out with this little idea, while thee dependency is exposed in an Interface method we could 
             * just create an Extension method that basically wraps the call injecting the dependency at method level
             * 
             * This would look something like this using a container to resolve the dependency:
             * 
             * ServiceLocator.Current.Resolve<my type>();
             * 
             * But this looks like we are using the Service Locator anti-pattern =( even though, we are placing the call 
             * in an extension method
             * 
             * Well I just wanted to experiment with this idea, any thoughts will be welcome
             * 
             * As conclusion there will be always trade-offs in each design decision so after evaluating choose the
             * solution that best fits your needs without requiring too much extra effort
             * 
             * */

            //// resolving the dependency without a container
            IAssemblyResourceFinder assemblyResourceFinderService = new AssemblyResourceFinder();

            //// calling the interface method
            return templateFactory.FindEmbeddedResource(assemblyResourceFinderService, embeddedFileName, resourceNamespace);
        }
    }
}
