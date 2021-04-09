namespace APPack
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using UnityEditor;
    using UnityEngine;

    public class APPackCreateEntityWindow : EditorWindow
    {
        public string EntityName;
        public bool CreateAnimationService;
        public bool CreateAudioService;
        public bool CreateEffectsService;
        public bool CreateUIService;
        public bool CreateNetworkService;

        [MenuItem("APPack/Architecture/Create Entity")]
        public static void CreateEntity()
        {
            var window = GetWindow<APPackCreateEntityWindow>(true, "Create Entity");
            window.Show();
        }
        
        public void OnGUI()
        {
            
            EditorGUILayout.LabelField("Entity Name:", EditorStyles.boldLabel);
            EntityName = EditorGUILayout.TextField(EntityName);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Create the following service(s):", EditorStyles.boldLabel);
            CreateAnimationService = EditorGUILayout.Toggle("Animation", CreateAnimationService);
            CreateAudioService = EditorGUILayout.Toggle("Audio", CreateAudioService);
            CreateEffectsService = EditorGUILayout.Toggle("Effects", CreateEffectsService);
            CreateUIService = EditorGUILayout.Toggle("UI", CreateUIService);
            CreateNetworkService = EditorGUILayout.Toggle("Network", CreateNetworkService);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Scaffolding", EditorStyles.boldLabel);
            if (EditorApplication.isCompiling)
                GUI.enabled = false;

            if(GUILayout.Button(new GUIContent("Generate Scripts")))
            {
                ScaffoldNewEntity();
            }
            else if(GUILayout.Button(new GUIContent("Create Prefab")))
            {
                CreatePrefab();
            }

            GUI.enabled = true;
        }
        
        private void ScaffoldNewEntity()
        {
            var templateKeywords = new Dictionary<string, string>
            {
                { "#NAMESPACENAME#", string.Format("{0}.{1}", PlayerSettings.productName, EntityName) },
                { "#ENTITYNAME#", EntityName },
                { "#ENTITYSERVICEFIELDS#", GetServiceFields() },
                { "#ENTITYONAWAKE#", GetOnAwake() },
                { "#ENTITYONSTART#", GetOnStart() }
            };
            var templatePath = string.Format("{0}/APPack/EntityArchitecture/Template", Application.dataPath);

            var entityPath = string.Format("{0}/{1}", Application.dataPath, EntityName);

            if (!Directory.Exists(entityPath))
            {
                Directory.CreateDirectory(entityPath);
            }
            else
            {
                Debug.Log("Entity already exists. You must first delete the existing entity or name it differently.");
                return;
            }

            var scriptFolderPath = string.Format("{0}/{1}", entityPath, "Scripts");

            if (!Directory.Exists(scriptFolderPath))
            {
                Directory.CreateDirectory(scriptFolderPath);
            }

            CreateEntityFile("Model", scriptFolderPath, templatePath, templateKeywords);
            CreateEntityFile("Bll", scriptFolderPath, templatePath, templateKeywords);
            CreateEntityFile("Controller", scriptFolderPath, templatePath, templateKeywords);
            CreateEntityFile("EventListener", scriptFolderPath, templatePath, templateKeywords);

            if (CreateAnimationService)
            {
                CreateEntityFile("AnimationService", scriptFolderPath, templatePath, templateKeywords);
                CreateEntityServiceDirectory("Animations", entityPath);
            }

            if (CreateAudioService)
            {
                CreateEntityFile("AudioService", scriptFolderPath, templatePath, templateKeywords);
                CreateEntityServiceDirectory("AudioClips", entityPath);
            }

            if (CreateEffectsService)
            {
                CreateEntityFile("EffectsService", scriptFolderPath, templatePath, templateKeywords);
            }

            if (CreateUIService)
            {
                CreateEntityFile("UIService", scriptFolderPath, templatePath, templateKeywords);
            }

            if (CreateNetworkService)
            {
                CreateEntityFile("NetworkService", scriptFolderPath, templatePath, templateKeywords);
            }

            AssetDatabase.Refresh();
        }

        public void CreatePrefab()
        {
            var entityGO = new GameObject(EntityName);

            entityGO.AddComponent<Transform>();
            //var model = entityGO.AddComponent(Types.GetType(string.Format("{0}.{1}.{1}Model", PlayerSettings.productName, EntityName), "Assembly-CSharp"));
            //var controller = entityGO.AddComponent(Types.GetType(string.Format("{0}.{1}.{1}Controller", PlayerSettings.productName, EntityName), "Assembly-CSharp"));
            //var bll = entityGO.AddComponent(Types.GetType(string.Format("{0}.{1}.{1}Bll", PlayerSettings.productName, EntityName), "Assembly-CSharp"));
            //controller.SetField("Bll", bll);
            //bll.SetField("Model", model);

            //CreateChildServices(entityGO, bll);

            var prefabPath = string.Format("Assets/{0}/{0}.prefab", EntityName);

            PrefabUtility.CreatePrefab(prefabPath, entityGO);

            DestroyImmediate(entityGO);
        }

        private void CreateChildServices(GameObject entityGO, Component bll)
        {
            if (CreateAnimationService)
            {
                var go = new GameObject(string.Format("{0}AnimationService", EntityName));
                //var service = go.AddComponent(Types.GetType(string.Format("{0}.{1}.{1}AnimationService", PlayerSettings.productName, EntityName), "Assembly-CSharp"));
                //bll.SetField("AnimationService", service);
                var animator = go.AddComponent<Animator>();
                //service.SetField(string.Format("{0}Animator", EntityName), animator);
                go.transform.parent = entityGO.transform;
            }

            if (CreateAudioService)
            {
                var go = new GameObject(string.Format("{0}AudioService", EntityName));
                //var service = go.AddComponent(Types.GetType(string.Format("{0}.{1}.{1}AudioService", PlayerSettings.productName, EntityName), "Assembly-CSharp"));
                //bll.SetField("AudioService", service);
                go.transform.parent = entityGO.transform;
            }

            if (CreateEffectsService)
            {
                var go = new GameObject(string.Format("{0}EffectsService", EntityName));
                //var service = go.AddComponent(Types.GetType(string.Format("{0}.{1}.{1}EffectsService", PlayerSettings.productName, EntityName), "Assembly-CSharp"));
                //bll.SetField("EffectsService", service);
                go.transform.parent = entityGO.transform;
            }

            if (CreateUIService)
            {
                var go = new GameObject(string.Format("{0}UIService", EntityName));
                //var service = go.AddComponent(Types.GetType(string.Format("{0}.{1}.{1}UIService", PlayerSettings.productName, EntityName), "Assembly-CSharp"));
                //bll.SetField("UIService", service);
                go.transform.parent = entityGO.transform;
            }

            //if (CreateNetworkService)
            //{
            //    var go = new GameObject(string.Format("{0}NetworkService", EntityName));
            //    //var service = go.AddComponent(Types.GetType(string.Format("{0}.{1}.{1}NetworkService", PlayerSettings.productName, EntityName), "Assembly-CSharp"));
            //    //bll.SetField("NetworkService", service);
            //    var networkIdentity = go.AddComponent<NetworkIdentity>();
            //    //service.SetField(string.Format("{0}NetworkIdentity", EntityName), networkIdentity);
            //    go.transform.parent = entityGO.transform;
            //}
        }

        private string GetServiceFields()
        {
            var result = new StringBuilder();
            result.Append(CreateAnimationService ? string.Format("\t\tpublic {0}AnimationService AnimationService;{1}", EntityName, Environment.NewLine) : "");
            result.Append(CreateAudioService ? string.Format("\t\tpublic {0}AudioService AudioService;{1}", EntityName, Environment.NewLine) : "");
            result.Append(CreateEffectsService ? string.Format("\t\tpublic {0}EffectsService EffectsService;{1}", EntityName, Environment.NewLine) : "");
            result.Append(CreateNetworkService ? string.Format("\t\tpublic {0}NetworkService NetworkService;{1}", EntityName, Environment.NewLine) : "");
            result.Append(CreateUIService ? string.Format("\t\tpublic {0}UIService UIService;{1}", EntityName, Environment.NewLine) : "");
            return result.ToString();
        }

        private string GetOnAwake()
        {
            var result = new StringBuilder();
            result.Append(CreateAnimationService ? string.Format("\t\t\tAnimationService.OnAwake();{0}", Environment.NewLine) : "");
            result.Append(CreateAudioService ? string.Format("\t\t\tAudioService.OnAwake();{0}", Environment.NewLine) : "");
            result.Append(CreateEffectsService ? string.Format("\t\t\tEffectsService.OnAwake();{0}", Environment.NewLine) : "");
            result.Append(CreateNetworkService ? string.Format("\t\t\tNetworkService.OnAwake();{0}", Environment.NewLine) : "");
            result.Append(CreateUIService ? string.Format("\t\t\tUIService.OnAwake();{0}", Environment.NewLine) : "");
            return result.ToString();
        }

        private string GetOnStart()
        {
            var result = new StringBuilder();
            result.Append(CreateAnimationService ? string.Format("\t\t\tAnimationService.OnStart();{0}", Environment.NewLine) : "");
            result.Append(CreateAudioService ? string.Format("\t\t\tAudioService.OnStart();{0}", Environment.NewLine) : "");
            result.Append(CreateEffectsService ? string.Format("\t\t\tEffectsService.OnStart();{0}", Environment.NewLine) : "");
            result.Append(CreateNetworkService ? string.Format("\t\t\tNetworkService.OnStart();{0}", Environment.NewLine) : "");
            result.Append(CreateUIService ? string.Format("\t\t\tUIService.OnStart();{0}", Environment.NewLine) : "");
            return result.ToString();
        }

        private void CreateEntityServiceDirectory(string type, string entityPath)
        {
            var entityServicePath = string.Format("{0}/{1}", entityPath, type);
            if (!Directory.Exists(entityServicePath))
                Directory.CreateDirectory(entityServicePath);
        }

        private void CreateEntityFile(string type, string scriptFolderPath, string templatePath, Dictionary<string, string> templateKeywords)
        {
            var path = string.Format("{0}/Entity{1}.cs.txt", templatePath, type);

            var template = File.ReadAllText(path);
            template = UpdateTemplate(template, templateKeywords);

            var newControllerPath = string.Format("{0}/{1}{2}.cs", scriptFolderPath, EntityName, type);
            File.WriteAllText(newControllerPath, template);

            AssetDatabase.ImportAsset(newControllerPath, ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);
        }

        private string UpdateTemplate(string template, Dictionary<string, string> templateKeywords)
        {
            foreach (var kvp in templateKeywords)
                template = template.Replace(kvp.Key, kvp.Value);

            return template;
        }
    }
}