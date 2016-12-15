using System;
using System.Text;

namespace Microsoft.Dynamics365.QualityTools.DataPopulation.ElementGenerators
{
    public class LoremIpsumGenerator
        : DataElementGenerator<string, LoremIpsumGenerationOptions>
    {
        public LoremIpsumGenerator()
            : base(LoremIpsumGenerationOptions.Default)
        {
        }

        public LoremIpsumGenerator(LoremIpsumGenerationOptions options)
            : base(options)
        {
        }

        public override string Generate(LoremIpsumGenerationOptions options)
        {
            var builder = new StringBuilder();
            var currentIndex = 0;

            switch (options.Length.Measure)
            {
                case TextMeasure.Letters:
                    while (currentIndex < options.Length.Length)
                    {
                        foreach (var paragraph in DataElementGenerator.Values.LoremIpsumText)
                        {
                            var length = options.Length.Length - currentIndex;

                            if (paragraph.Length <= length)
                            {
                                builder.AppendLine(paragraph);
                                currentIndex += paragraph.Length;
                            }
                            else
                            {
                                builder.AppendLine(paragraph.Substring(1, length));
                                currentIndex += length;

                                break;
                            }
                        }
                    }

                    break;
                case TextMeasure.Words:
                    while (currentIndex < options.Length.Length)
                    {
                        foreach (var paragraph in DataElementGenerator.Values.LoremIpsumText)
                        {
                            var words = paragraph.Split(' ');

                            foreach (string w in words)
                            {
                                builder.Append(w);
                                builder.Append(" ");

                                currentIndex++;

                                if (currentIndex == options.Length.Length)
                                    break;
                            }
                        }
                    }

                    break;
                case TextMeasure.Paragraphs:
                    while (currentIndex < options.Length.Length)
                    {
                        foreach (var paragraph in DataElementGenerator.Values.LoremIpsumText)
                        {
                            builder.AppendLine(paragraph);

                            currentIndex++;

                            if (currentIndex == options.Length.Length)
                                break;
                        }
                    }

                    break;
            }

            return builder.ToString();
        }
    }

    public enum TextMeasure
    {
        Letters,
        Words,
        Paragraphs
    }

    public class TextLength
    {
        public TextLength(Int32 length, TextMeasure measure)
        {
            this.Length = length;
            this.Measure = measure;
        }

        public Int32 Length { get; private set; }

        public TextMeasure Measure { get; private set; }
    }

    public class LoremIpsumGenerationOptions
        : DataElementGenerationOptions
    {
        public TextLength Length { get; set; }

        public static LoremIpsumGenerationOptions Default => new LoremIpsumGenerationOptions { Length = new TextLength(100, TextMeasure.Words) };
    }

    public static partial class DataElementGenerator
    {
        public static partial class Values
        {
            public static string[] LoremIpsumText = new string[]
            {
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus aliquam turpis et feugiat tincidunt. Quisque vulputate, mi in fringilla tincidunt, augue justo tincidunt lacus, nec imperdiet ex eros id magna. Etiam aliquet quam sit amet nunc condimentum, ac semper odio pretium. Praesent accumsan sem nec libero elementum suscipit. Cras bibendum felis vel nibh viverra dignissim. Fusce a diam eget sem ultrices blandit. Mauris accumsan maximus erat, sed iaculis odio feugiat eget. Praesent venenatis quam a metus placerat pulvinar. Suspendisse pellentesque tempor elit vitae porta. Nulla in iaculis eros. Mauris efficitur lacinia elit, nec mattis nibh. Nunc id ex a massa lacinia condimentum. Maecenas eget tellus quis risus aliquam posuere sit amet eget ex. Suspendisse potenti. Nulla posuere, lectus a tincidunt sagittis, purus risus condimentum nibh, id mattis libero lectus nec mauris. Ut non augue convallis, molestie sem ut, lobortis sem.",
                "Vestibulum sit amet magna rutrum nisl eleifend interdum ac ut neque. Sed in ligula sodales, tincidunt ante in, pellentesque massa. Pellentesque blandit blandit nulla, id tincidunt lacus pretium eu. Nunc nec dignissim diam. Suspendisse enim risus, suscipit et magna vel, laoreet mollis velit. Vivamus congue porttitor felis a gravida. Sed ac vehicula sem, quis interdum sapien. Pellentesque luctus ante in magna cursus, eget gravida est dignissim. Integer ligula odio, mollis ut iaculis ut, sagittis et elit. Interdum et malesuada fames ac ante ipsum primis in faucibus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Integer feugiat mauris eu mauris sodales, vel viverra purus vehicula. Cras et sollicitudin eros. Vivamus non nibh id ligula condimentum ornare at id mi. Ut sed ipsum ac quam suscipit sodales sit amet et sapien.",
                "Donec iaculis dolor at dui cursus porttitor. Sed volutpat suscipit ultrices. Vestibulum eros nisi, luctus et turpis non, fermentum sodales lectus. Fusce pharetra tortor sed ante tempor semper. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris placerat nibh purus, ac commodo ex congue et. Fusce a tellus sed nulla imperdiet ullamcorper. Aliquam erat volutpat. Curabitur quis augue ut sem interdum porttitor nec eu velit.",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam in libero porttitor, accumsan urna et, consectetur felis. Morbi vel rhoncus risus. Cras sit amet risus dui. Etiam malesuada sapien eu libero bibendum rutrum. Morbi sagittis mollis ligula vel scelerisque. Praesent viverra justo libero, eu consectetur nibh accumsan at. Sed vel erat lorem.",
                "Curabitur id iaculis purus. Cras dolor elit, rutrum ac pellentesque non, porttitor non ex. Sed maximus lacus eget nunc accumsan dictum. Cras fringilla, metus eget finibus sagittis, ipsum magna pulvinar risus, ac condimentum lacus eros vitae diam. Vivamus sed tellus pellentesque, dictum nisl ut, mattis diam. Quisque tempor tortor vel erat luctus, vel finibus metus elementum. Nullam dignissim lacus vel ipsum venenatis vulputate. Donec ac fringilla metus. Aliquam sit amet ante sem. Curabitur id ultricies nulla. Aliquam maximus ornare varius. Nam vitae libero lacus.",
                "Ut accumsan viverra ex, eu placerat ex auctor sit amet. Donec id sodales elit. Suspendisse maximus elit pretium neque aliquam dictum non id orci. Donec massa neque, porta in augue id, varius convallis dui. In hac habitasse platea dictumst. Sed massa nunc, dignissim vitae suscipit quis, rhoncus nec magna. Aenean efficitur mollis est ac cursus. Phasellus tempor eros sem, quis egestas neque commodo at. Duis vehicula nulla id orci varius aliquet. Suspendisse lacinia nisl nec elementum ultrices. Praesent non rutrum urna. Aenean in ornare tellus. Duis a arcu nunc.",
                "Nunc vehicula velit vel luctus ultrices. Duis nisi arcu, consequat sit amet consequat vel, eleifend quis lectus. Sed mattis, eros id convallis scelerisque, lacus nisl dignissim eros, sit amet sagittis dolor elit dignissim mauris. Suspendisse porttitor, lorem eu dictum lobortis, nulla dui tincidunt ante, at laoreet ipsum quam sed est. Etiam augue orci, ultricies vel tincidunt nec, consequat quis nulla. Vestibulum egestas varius felis, eget tristique velit condimentum vel. Mauris pellentesque feugiat lorem, quis ultricies lacus varius in. Mauris aliquet lacus blandit ex mollis egestas. Nam vestibulum mauris sit amet nunc tempor facilisis. Etiam non posuere mi.",
                "Nullam eros magna, tempus ac libero at, consectetur vehicula eros. Phasellus rhoncus, sapien vel malesuada sagittis, leo magna euismod magna, id ultrices turpis velit a sem. Nam quis felis lorem. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vitae lacus erat. Ut blandit dictum iaculis. Etiam dolor arcu, mollis eget consectetur ut, viverra ut dolor. Duis ac facilisis ante, nec porttitor ligula.",
                "Quisque sit amet facilisis nisi. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus imperdiet, nunc vel varius eleifend, quam enim lacinia risus, eget porttitor augue felis vel elit. Vestibulum feugiat, velit vitae pulvinar rutrum, est lorem placerat leo, et rhoncus dolor ligula vitae nisl. Vivamus vel dolor a enim vulputate vulputate et non ante. Nulla facilisi. Duis neque massa, imperdiet a orci sit amet, accumsan mattis tellus. Nulla orci urna, posuere vitae rutrum venenatis, lacinia vitae turpis. Duis ligula est, posuere at diam id, tempor eleifend eros. Nulla tincidunt augue at consectetur scelerisque. Pellentesque eros libero, porta porta pretium quis, porttitor ut sapien. Donec venenatis ultrices scelerisque.",
                "Nunc mollis tincidunt fermentum. Donec suscipit urna ligula, et viverra tellus scelerisque in. Sed varius elit eu gravida ultrices. Integer porttitor tellus eget sagittis pretium. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Cras volutpat ligula sed tellus aliquet scelerisque. Sed aliquam pellentesque aliquet. Nulla quis sapien fermentum, malesuada mi nec, pretium sem. Aenean in elit in neque tempus auctor. Duis vitae pulvinar lectus. Mauris congue, ex non vehicula sagittis, nisl libero dapibus ipsum, at convallis mauris mauris sit amet nibh. Donec hendrerit enim sit amet laoreet tempor. Vestibulum a velit at nulla maximus semper ut et metus.",
                "Aliquam aliquam faucibus consequat. Nam placerat purus risus, a consequat libero malesuada eu. In posuere metus ipsum, eget tincidunt erat varius et. Integer libero ante, elementum lacinia tortor nec, efficitur placerat eros. Sed ultricies varius turpis ac hendrerit. Vivamus consequat tincidunt vestibulum. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Sed placerat felis sed gravida laoreet. Ut molestie neque quis convallis sagittis. Cras non leo in tortor finibus ultricies et eget arcu.",
                "Etiam et viverra libero. Nullam ultrices, metus ut dapibus pharetra, turpis nibh venenatis arcu, eu convallis tellus sem eget magna. Integer id facilisis purus, id bibendum nisl. Aenean ut hendrerit nisi, dignissim finibus diam. Etiam ut cursus nulla. Sed pretium eros a nibh ornare elementum. Praesent lacinia lectus metus, a rhoncus velit euismod vitae. Sed congue metus dictum suscipit lacinia.",
                "Vivamus varius, lorem non convallis mattis, nisl massa auctor odio, eget feugiat erat diam eu massa. Quisque sit amet metus eget nulla posuere sodales et vel dui. Morbi maximus hendrerit magna, hendrerit bibendum dolor porta sed. Integer sed urna in justo rhoncus tincidunt id ut massa. Nulla vel elit vel neque euismod ullamcorper. Quisque in tincidunt magna. Vivamus feugiat viverra lectus sagittis eleifend. Maecenas sit amet erat erat.",
                "Sed in lorem rhoncus, mattis nisi vitae, porta eros. Nam dolor sapien, fringilla sed sapien sit amet, placerat rhoncus leo. Suspendisse potenti. Donec justo augue, aliquam vel volutpat sed, eleifend quis elit. Mauris commodo justo id aliquet varius. Proin varius dapibus mi. Maecenas pretium arcu ac tortor cursus facilisis. Phasellus lacinia, lacus non gravida lobortis, tellus augue convallis nulla, nec iaculis orci lectus sit amet tortor. Donec tempus ex ante, convallis suscipit ante efficitur id. Maecenas imperdiet, tellus in maximus suscipit, mauris dui posuere nisl, non porttitor lectus odio sit amet turpis. Sed convallis mauris eros, id venenatis lacus rhoncus sit amet. Vivamus convallis metus odio, placerat vestibulum velit tristique sit amet. Sed congue augue augue, id mattis nisi aliquam nec.",
                "Donec tincidunt, velit nec volutpat suscipit, nibh ante dictum orci, vel volutpat arcu dolor non mi. Sed a mauris quis neque gravida dapibus eget ac magna. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nunc vel ipsum eu lorem eleifend gravida. Fusce sed erat elementum ipsum ullamcorper vestibulum a vitae lectus. Mauris id mi lacus. Donec finibus ut nisl in congue. Integer purus justo, consequat in lectus nec, sagittis congue neque. Fusce accumsan, purus vel viverra venenatis, ex lorem egestas urna, dignissim aliquam nisl libero quis lectus. Cras risus libero, luctus a sem at, pellentesque commodo mi.",
                "Nam tempus interdum ipsum vel tempor. Suspendisse fringilla tortor quis velit porttitor, sagittis bibendum ex vulputate. Nunc ipsum neque, efficitur sit amet fringilla eget, posuere in turpis. Aenean lacinia sapien non ipsum viverra rhoncus. Suspendisse nec fringilla metus. Nam commodo vel ante eu finibus. Nulla viverra sem nibh, eu viverra mi vehicula in. Mauris euismod porta ligula, eu imperdiet sapien tincidunt vel. Morbi sit amet arcu faucibus, varius dolor in, placerat turpis. Donec malesuada laoreet purus et eleifend. Aliquam aliquet tempus magna, eget efficitur odio accumsan at. Nulla viverra lacus sed facilisis auctor. In rhoncus varius tempus. Curabitur auctor tincidunt eros, ultricies interdum mauris porta eu. Aenean vehicula lacinia sapien, in aliquam massa. Nulla feugiat tortor velit, eu tempus neque dignissim a.",
                "Fusce condimentum, tortor id sagittis lobortis, nulla nisl rutrum leo, sed lacinia ex massa malesuada massa. Phasellus nec justo pulvinar, aliquam mauris eu, volutpat sapien. Quisque vulputate est in eros accumsan, et vehicula mi bibendum. Integer accumsan orci eget pharetra bibendum. Etiam malesuada diam congue dignissim blandit. Cras pharetra tincidunt luctus. Integer vulputate ligula accumsan sapien sodales, sed blandit sem viverra. Sed tincidunt non ex eu sollicitudin. Suspendisse vestibulum ligula sit amet volutpat ullamcorper. Fusce efficitur fermentum tempor. Maecenas imperdiet, nunc sed lobortis lobortis, nisi purus eleifend urna, in mollis magna libero id diam. Fusce et diam ac augue bibendum fermentum quis ac massa. Phasellus maximus risus odio, ultrices tristique mi bibendum non. Donec pellentesque, tellus sit amet condimentum porttitor, sapien risus semper nibh, eu lacinia lorem nibh eget nibh. In id quam a nibh imperdiet bibendum interdum in sem. Mauris pulvinar, purus nec aliquet interdum, lacus tortor hendrerit urna, at consequat ligula nisl at risus.",
                "Phasellus ut tortor quis elit facilisis ultricies. Suspendisse dignissim odio sed nunc hendrerit, quis maximus augue sodales. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nullam sit amet leo vel ipsum aliquet mollis. Donec rutrum elit vel lacus molestie, eget porttitor diam consectetur. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Proin fermentum faucibus orci sit amet vehicula. Proin nec dignissim nulla. Praesent tristique lacus nisl, id bibendum tortor rhoncus ac. Nam sed dui in tortor blandit tincidunt. Nunc pharetra elit ultrices, blandit odio vel, bibendum ante. Mauris ullamcorper dolor vitae erat lobortis, a accumsan arcu varius. Quisque ipsum diam, vehicula eget purus ac, pretium elementum risus. Nam consequat metus dignissim volutpat sodales. Vivamus dictum pulvinar nisl, eget dignissim eros elementum vel. Nunc ut euismod lectus, id vulputate tortor.",
                "Suspendisse potenti. Phasellus lobortis neque a dui sagittis, ac facilisis massa rutrum. Mauris id lorem at lorem blandit euismod. Ut at dolor imperdiet ligula auctor pulvinar vitae vitae ante. Nullam in felis vitae dolor commodo rhoncus vel et est. Etiam id risus a lacus rhoncus malesuada. Integer ultricies accumsan augue. Phasellus vel tincidunt ante. Proin velit sapien, ornare sit amet lacus sed, sagittis iaculis tortor. Quisque quis ex non arcu aliquet faucibus at nec dui.",
                "Donec cursus porttitor justo, vitae rhoncus justo laoreet a. Pellentesque hendrerit laoreet purus nec venenatis. Aliquam erat volutpat. Nam egestas, orci id tincidunt consequat, sem dui tristique dolor, eu sagittis nisl leo id eros. Ut lorem mauris, ultricies sit amet tincidunt nec, mollis ut ligula. Nullam porttitor rutrum ligula et suscipit. Fusce aliquam accumsan tortor a sollicitudin. Cras ac egestas dolor. Sed ultrices convallis est, at venenatis ante eleifend quis. Curabitur dolor massa, fermentum sed ligula ut, imperdiet auctor sem. Nullam interdum lacinia ante. Duis cursus ex est, vel viverra nunc vehicula sed. Pellentesque quis luctus velit. Vivamus fermentum mi sit amet imperdiet tincidunt.",
                "Aliquam quis purus eu ante molestie sagittis. Morbi luctus mi in ornare suscipit. Nulla aliquet ante risus, ac pretium sapien volutpat sed. Maecenas et hendrerit eros. Nunc facilisis id tellus sit amet molestie. Nullam gravida ligula non arcu accumsan, ac sagittis lorem fermentum. Morbi ipsum mi, tincidunt eget varius et, sagittis sit amet justo. Aenean tempor risus orci, id facilisis odio feugiat non. Donec ac elit tristique, tristique tortor in, vulputate massa. Aliquam erat volutpat. Nulla eu urna eu lectus mollis ultrices. Morbi at quam non urna congue tincidunt. In ac purus nibh. Nullam posuere suscipit odio. Mauris hendrerit mi tellus, non porttitor velit molestie vel. Aenean et justo blandit, tincidunt ligula et, ultrices metus.",
                "Quisque fringilla nibh eu mauris consequat tempus. Nam vitae velit vitae orci sollicitudin tristique ac eu sapien. Nullam blandit velit arcu, mollis maximus metus maximus eu. Praesent sed egestas urna, eget pretium neque. Praesent justo orci, commodo et massa ut, bibendum mollis eros. Nam commodo neque at vestibulum viverra. Etiam id dignissim lorem, nec accumsan nisi. Donec a lectus ipsum. Ut eu ornare elit. Suspendisse et ultrices mi. Nam ultrices elit eget mi dapibus faucibus. Sed condimentum est vel ante scelerisque facilisis. Praesent ultricies elit ut finibus congue.",
                "Phasellus congue lectus vel sapien dictum, id dictum ante mattis. Donec nec diam et erat suscipit bibendum. Duis convallis, dui ac bibendum blandit, orci orci pretium nisl, sed sodales felis est ac purus. Cras mauris tellus, molestie molestie aliquam semper, scelerisque et sem. Sed laoreet vulputate lectus sed blandit. Nullam in lorem fringilla, fermentum dolor eget, consectetur justo. Maecenas maximus luctus tellus. Curabitur vehicula tincidunt ante, vehicula rutrum urna semper sed. Maecenas aliquam aliquam pretium."
            };
        }
    }
}