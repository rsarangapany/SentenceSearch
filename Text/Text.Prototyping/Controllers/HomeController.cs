using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Text.Extract;
using Text.Extract.Factory;
using Text.Extract.Tag;
using Text.NaturalLanguageProcessing;
using Text.Prototyping.Models;
using Text.Search;
using Text.Search.Factory;

namespace Text.Prototyping.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var model = new HomeViewModel
            {
                DocumentText = @"Howard Roark laughed. He stood naked at the edge of a cliff. The lake lay far below him. A frozen explosion of granite burst in flight to the sky over motionless water. The water seemed immovable, the stone flowing. The stone had the stillness of one brief moment in battle when thrust meets thrust and the currents are held in a pause more dynamic than motion. The stone glowed, wet with sunrays.
The lake below was only a thin steel ring that cut the rocks in half. The rocks went on into the depth, unchanged. They began and ended in the sky. So that the world seemed suspended in space, an island floating on nothing, anchored to the feet of the man on the cliff.
His body leaned back against the sky. It was a body of long straight lines and angles, each curve broken into planes. He stood, rigid, his hands hanging at his sides, palms out. He felt his shoulder blades drawn tight together, the curve of his neck, and the weight of the blood in his hands. He felt the wind behind him, in the hollow of his spine. The wind waved his hair against the sky. His hair was neither blond nor red, but the exact color of ripe orange rind.
He laughed at the thing which had happened to him that morning and at the things which now lay ahead.
He knew that the days ahead would be difficult. There were questions to be faced and a plan of action to be prepared. He knew that he should think about it. He knew also that he would not think, because everything was clear to him already, because the plan had been set long ago, and because he wanted to laugh.
He tried to consider it. But he forgot. He was looking at the granite.
He did not laugh as his eyes stopped in awareness of the earth around him. His face was like a law of nature — a thing one could not question, alter or implore. It had high cheekbones over gaunt, hollow cheeks; gray eyes, cold and steady; a contemptuous mouth, shut tight, the mouth of an executioner or a saint.
He looked at the granite. To be cut, he thought, and made into walls. He looked at a tree. To be split and made into rafters. He looked at a streak of rust on the stone and thought of iron ore under the ground. To be melted and to emerge as girders against the sky.
These rocks, he thought, are here for me; waiting for the drill, the dynamite and my voice; waiting to be split, ripped, pounded, reborn; waiting for the shape my hands will give them.
Then he shook his head, because he remembered that morning and that there were many things to be done. He stepped to the edge, raised his arms, and dived down into the sky below.
He cut straight across the lake to the shore ahead. He reached the rocks where he had left his clothes. He looked regretfully about him. For three years, ever since he had lived in Stanton, he had come here for his only relaxation, to swim, to rest, to think, to be alone and alive, whenever he could find one hour to spare, which had not been often. In his new freedom the first thing he had wanted to do was to come here, because he knew that he was coming for the last time. That morning he had been expelled from the Architectural School of the Stanton Institute of Technology.
He pulled his clothes on: old denim trousers, sandals, a shirt with short sleeves and most of its buttons missing. He swung down a narrow trail among the boulders, to a path running through a green slope, to the road below.
He walked swiftly, with a loose, lazy expertness of motion. He walked down the long road, in the sun. Far ahead Stanton lay sprawled on the coast of Massachusetts, a little town as a setting for the gem of its existence — the great institute rising on a hill beyond.
The township of Stanton began with a dump. A gray mound of refuse rose in the grass. It smoked faintly. Tin cans glittered in the sun. The road led past the first houses to a church. The church was a Gothic monument of shingles painted pigeon blue. It had stout wooden buttresses supporting nothing. It had stained-glass windows with heavy 
traceries of imitation stone. It opened the way into long streets edged by tight, exhibitionist lawns. Behind the lawns stood wooden piles tortured out of all shape: twisted into gables, turrets, dormers; bulging with porches; crushed under huge, sloping roofs. White curtains floated at the windows. A garbage can stood at a side door, flowing over. An old Pekinese sat upon a cushion on a door step, its mouth drooling. A line of diapers fluttered in the wind between the columns of a porch.
People turned to look at Howard Roark as he passed. Some remained staring after him with sudden resentment. They could give no reason for it: it was an instinct his presence awakened in most people. Howard Roark saw no one. For him, the streets were empty. He could have walked there naked without concern.
He crossed the heart of Stanton, a broad green edged by shop windows. The windows displayed new placards announcing: WELCOME TO THE CLASS OF '22! GOOD LUCK, CLASS OF '22! The Class of '22 of the Stanton Institute of Technology was holding its commencement exercises that afternoon.",
                AnoymisedText = "face,nature,exist",
                SearchText = "wooden law of nature"
            };

            return View(model);
        }

        public ActionResult Search(HomeViewModel model)
        {
            var languageModelPaths = $"{AppDomain.CurrentDomain.BaseDirectory}Bin\\Resource\\Models";

            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.RegisterNaturalLanguageProcessingService(languageModelPaths);
            serviceCollection.RegisterSearchService();
            serviceCollection.RegisterExtractService();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var textServiceFactory = serviceProvider.GetService<ITextSearchFactory>();

            var textExtractionServiceFactory = serviceProvider.GetService<ITextExtractFactory>();

            var extractTags = new IExtractTag[]
            {
                new FoundTag("Found"),
                new AnonymiseTag("Anonymised", "*******", model.AnoymisedText.Split(','), textServiceFactory.TextService)
            };

            var snippets = textExtractionServiceFactory.TextExtract.ExtractSnippets(model.DocumentText, model.SearchText, extractTags);

            model.SearchResults = snippets.Select(snippet => new HomeViewModel.SearchViewModel {Rank = snippet.Rank, Text = snippet.Text}).ToArray();

            return View("~/Views/Home/Index.cshtml", model);
        }
    }
}