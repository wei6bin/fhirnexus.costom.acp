using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Ihis.FhirEngine.Core.Handlers.Data;
using Ihis.FhirEngine.Data.Models;

namespace Synapxe.FhirWebApi.Relational.Data;

public class QuestionnaireDataMapper : IFhirDataMapper<QuestionnaireEntity, Questionnaire>
{
    public Questionnaire Map(QuestionnaireEntity resource)
    {
        var questionnaire = new Questionnaire
        {
            Id = resource.Id.ToString(),
            Status = EnumUtility.ParseLiteral<PublicationStatus>(resource.Status),
            Title = resource.Title,
            Meta = new Meta
            {
                LastUpdated = DateTimeOffset.Now,
                VersionId = "1",
            }
        };

        var itemDictionary = resource.Item.ToDictionary(i => i.LinkId);
        foreach (var item in resource.Item)
        {
            if (string.IsNullOrEmpty(item.ParentLinkId))
            {
                questionnaire.Item.Add(ConvertToItemComponent(item, itemDictionary));
            }
        }

        return questionnaire;
    }

    private static Questionnaire.ItemComponent ConvertToItemComponent(QuestionnaireEntity.ItemComponent entityItem, Dictionary<string, QuestionnaireEntity.ItemComponent> itemDictionary)
    {
        var itemComponent = new Questionnaire.ItemComponent
        {
            LinkId = entityItem.LinkId,
            Text = entityItem.Text,
            Type = EnumUtility.ParseLiteral<Questionnaire.QuestionnaireItemType>(entityItem.Type),
            Required = entityItem.Required,
        };

        foreach (var answerOption in entityItem.AnswerOption)
        {
            itemComponent.AnswerOption.Add(new Questionnaire.AnswerOptionComponent
            {
                Value = new Coding
                {
                    Code = (answerOption.Value.Value5).Code,
                    Display = (answerOption.Value.Value5).Display,
                },
            });
        }

        foreach (var enableWhen in entityItem.EnableWhen)
        {
            var x = enableWhen.Answer.Coding.Code;
            itemComponent.EnableWhen.Add(new Questionnaire.EnableWhenComponent
            {
                Question = enableWhen.Question,
                Operator = EnumUtility.ParseLiteral<Questionnaire.QuestionnaireItemOperator>(enableWhen.Operator),
                Answer = new Coding()
                {
                    Code = enableWhen.Answer.Coding.Code,
                    Display = enableWhen.Answer.Coding.Display,
                },
            });
        }

        var subItems = itemDictionary.Values.Where(i => i.ParentLinkId == entityItem.LinkId).ToList();
        foreach (var subItem in subItems)
        {
            itemComponent.Item.Add(ConvertToItemComponent(subItem, itemDictionary));
        }

        return itemComponent;
    }

    public QuestionnaireEntity ReverseMap(Questionnaire resource)
    {
        var entity = new QuestionnaireEntity
        {
            Id = resource.Id,
            VersionId = int.TryParse(resource.VersionId, out var vid) ? vid : 0,
            LastUpdated = resource.Meta?.LastUpdated,
            Status = resource.Status.GetLiteral(),
            Title = resource.Title,
        };

        entity.Item = new List<QuestionnaireEntity.ItemComponent>();

        foreach (var item in resource.Item)
        {
            FlattenQuestionnaireItemsRecursive(null, item, entity.Item);
        }

        return entity;
    }

    private static void FlattenQuestionnaireItemsRecursive(Questionnaire.ItemComponent parentItem, Questionnaire.ItemComponent childItem, List<QuestionnaireEntity.ItemComponent> result)
    {
        if (childItem == null) return;

        var childEntity = new QuestionnaireEntity.ItemComponent
        {
            LinkId = childItem.LinkId,
            Text = childItem.Text,
            Type = childItem.Type.GetLiteral(),
            Required = childItem.Required,
        };

        childEntity.AnswerOption = [];
        foreach (var answerOption in childItem.AnswerOption)
        {
            childEntity.AnswerOption.Add(new QuestionnaireEntity.AnswerOptionComponent
            {
                Value = new DataEntity<int?, DateEntity, TimeEntity, string?, CodingEntity, ResourceReferenceEntity>
                {
                    TypeName = DataTypeName.Coding,
                    Value5 = new CodingEntity
                    {
                        Code = (answerOption.Value as Coding).Code,
                        Display = (answerOption.Value as Coding).Display,
                    }
                }
            });
        }

        childEntity.EnableWhen = new List<QuestionnaireEntity.EnableWhenComponent>();
        foreach (var enableWhen in childItem.EnableWhen)
        {
            childEntity.EnableWhen.Add(new QuestionnaireEntity.EnableWhenComponent
            {
                Question = enableWhen.Question,
                Operator = enableWhen.Operator.GetLiteral(),
                Answer = new DataEntity()
                {
                    TypeName = EnumUtility.ParseLiteral<DataTypeName>(enableWhen.Answer.TypeName)!.Value,
                    Coding = new CodingEntity
                    {
                        Code = (enableWhen.Answer as Coding).Code,
                        Display = (enableWhen.Answer as Coding).Display,
                    }
                }
            });
        }

        if (parentItem != null)
        {
            childEntity.ParentLinkId = parentItem.LinkId;
        }

        result.Add(childEntity);

        foreach (var subItem in childItem.Item)
        {
            FlattenQuestionnaireItemsRecursive(childItem, subItem, result);
        }
    }
}
