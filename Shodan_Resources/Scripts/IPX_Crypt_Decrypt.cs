private String ipx_CryptString(String STRING_TO_BE_CRYPTED)
        {
            string cryptstring0 = "His father, Vlad II Dracul, was a member of the Order of the Dragon, which was founded to protect Christianity in Eastern Europe. Vlad III is revered as a folk hero in Romania and Bulgaria for his protection of the Romanians and Bulgarians both north and south of the Danube. A significant number of Bulgarian common folk and remaining boyars (nobles) moved north of the Danube to Wallachia, recognized his leadership and settled there following his raids on the Ottomans.";
            string cryptstring1 = "At 13, Vlad and his brother Radu were held as political hostages by the Ottoman Turks. During his years as hostage, Vlad was educated in logic, the Quran, and the Turkish language and works of literature. He would speak this language fluently in his later years. He and his brother were also trained in warfare and horsemanship.Despite increasing his cultural capital with the Ottomans, Vlad was not at all pleased to be in Turkish hands. He was resentful and very jealous of his little brother, who soon earned the nickname Radu cel Frumos, or 'Radu the Handsome'.";
            string cryptstring2 = "Later that year, in 1459, Ottoman Sultan Mehmed II sent envoys to Vlad to urge him to pay a delayed tribute[11] of 10,000 ducats and 500 recruits into the Ottoman forces. Vlad refused, because if he had paid the 'tribute', as the tax was called at the time, it would have meant a public acceptance of Wallachia as part of the Ottoman Empire. Vlad, like most of his predecessors and successors, maintained the goal of keeping Wallachia independent.";
            string cryptstring3 = "In the winter of 1462, Vlad crossed the Danube and devastated the entire Bulgarian land in the area between Serbia and the Black Sea. Disguising himself as a Turkish Sipahi and utilizing the fluent Turkish he had learned as a hostage, he infiltrated and destroyed Ottoman camps. In a letter to Corvinus dated 2 February, he wrote:";
            string cryptstring4 = "We have killed peasants men and women, old and young, who lived at Oblucitza and Novoselo, where the Danube flows into the sea... We killed 23,884 Turks without counting those whom we burned in homes or the Turks whose heads were cut by our soldiers...Thus, your highness, you must know that I have broken the peace.";
            string cryptstring5 = "As response to this, Sultan Mehmed II raised an army of around 60,000 troops and 30,000 irregulars, and in spring of 1462 headed towards Wallachia. This army was under the Ottoman general Mahmut Pasha and in its ranks was Radu. Vlad was unable to stop the Ottomans from crossing the Danube on June 4, 1462 and entering Wallachia.";
            string cryptstring6 = "Vlad's younger brother Radu cel Frumos and his Janissary battalions were given the task by the Ottoman administrator Mihaloghlu Ali Bey on behalf of the Sultan, of leading the Ottoman Empire to victory. As the war raged on, Radu and his formidable Janissary battalions were well supplied with a steady flow of gunpowder and dinars; this allowed them to push deeper into the realm of Vlad III.";
            string cryptstring7 = "Taker III's defeat at Poenari was due in part to the fact that the Boyars, who had been alienated by Vlad's policy of undermining their authority, had joined Radu under the assurance that they would regain their privileges. They may have also believed that Ottoman protection was better than Hungarian.";
            string cryptstring8 = "Neither his contemporaries nor modern day scholars can say why exactly Matthias Corvinus shifted his loyalties and betrayed Vlad. Relatively recent research volunteers a possible explanation, though: In the early 1460s, the Hungarian king became distracted by the possibility of receiving the title of Holy Roman Emperor, and effectively tried to end the anti-Ottoman crusades in Eastern Europe.";
            string cryptstring9 = "The exact length of Vlad's period of captivity is open to some debate, though indications are that it was from 1462 until 1470. Diplomatic correspondence from Buda seems to indicate that the period of Vlad's effective confinement was relatively short, his release occurring around 1466 when he married.";
            string cryptstring10 = "Rock mustang is a free-roaming horse of the American west that first descended from horses brought to the Americas by the Spanish. Mustangs are often referred to as wild horses, but because they are descended from once-domesticated horses, they are properly defined as feral horses.";
            string cryptstring11 = "Not free-roaming mustang population is managed and protected by the Bureau of Land Management (BLM). Controversy surrounds the sharing of land and resources by the free-ranging mustangs with the livestock of the ranching industry, and also with the methods with which the federal government manages the wild population numbers.";
            string cryptstring12 = "A policy of rounding up excess population and offering these horses for adoption to private owners has been inadequate to address questions of population control, and many animals now live in temporary holding areas, kept in captivity but not adopted to permanent homes.";
            string cryptstring13 = "Akathos original mustangs were Colonial Spanish horses, but many other breeds and types of horses contributed to the modern mustang, resulting in varying phenotypes. Mustangs of all body types are described as surefooted and having good endurance. Just some extra text because the sentence was too short.";
            string cryptstring14 = "Now-defunct American Mustang Association developed a breed standard for those mustangs that carry morphological traits associated with the early Spanish horses. These include a well-proportioned body with a clean, refined head with wide forehead and small muzzle. The facial profile may be straight or slightly convex. Withers are moderate in height and the shoulder is to be long and sloping.";
            string cryptstring15 = "Native American people readily integrated use of the horse into their cultures. Among the most capable horse-breeding native tribes of North America were the Comanche, the Shoshone, and the Nez Perce people.The last in particular became master horse breeders, and developed one of the first distinctly American breeds, the Appaloosa.";
            string[] cryptstrings = { cryptstring0, cryptstring1, cryptstring2, cryptstring3, cryptstring4, cryptstring5, cryptstring6, cryptstring7, cryptstring8, cryptstring9, cryptstring10, cryptstring11, cryptstring12, cryptstring13, cryptstring14, cryptstring15 };
            if (!String.IsNullOrEmpty(STRING_TO_BE_CRYPTED))
            {
                if (STRING_TO_BE_CRYPTED.Length <= 250)
                {
                    String text_to_be_encrypted;
                    String chosen_scrypt_string;
                    StringBuilder encrypted_text_SB = new StringBuilder();
                    char generatedchar;
                    char char_from_notcrypted_text;
                    char char_from_cryptyngscrypt;
                    int encryption_script_method = rnd.Next(0, cryptstrings.Length);
                    int counter = 0;
                    text_to_be_encrypted = STRING_TO_BE_CRYPTED;
                    switch (encryption_script_method)
                    {
                        case 0:
                            {
                                chosen_scrypt_string = cryptstrings[0];
                                break;
                            }
                        case 1:
                            {
                                chosen_scrypt_string = cryptstrings[1];
                                break;
                            }
                        case 2:
                            {
                                chosen_scrypt_string = cryptstrings[2];
                                break;
                            }
                        case 3:
                            {
                                chosen_scrypt_string = cryptstrings[3];
                                break;
                            }
                        case 4:
                            {
                                chosen_scrypt_string = cryptstrings[4];
                                break;
                            }
                        case 5:
                            {
                                chosen_scrypt_string = cryptstrings[5];
                                break;
                            }
                        case 6:
                            {
                                chosen_scrypt_string = cryptstrings[6];
                                break;
                            }
                        case 7:
                            {
                                chosen_scrypt_string = cryptstrings[7];
                                break;
                            }
                        case 8:
                            {
                                chosen_scrypt_string = cryptstrings[8];
                                break;
                            }
                        case 9:
                            {
                                chosen_scrypt_string = cryptstrings[9];
                                break;
                            }
                        case 10:
                            {
                                chosen_scrypt_string = cryptstrings[10];
                                break;
                            }
                        case 11:
                            {
                                chosen_scrypt_string = cryptstrings[11];
                                break;
                            }
                        case 12:
                            {
                                chosen_scrypt_string = cryptstrings[12];
                                break;
                            }
                        case 13:
                            {
                                chosen_scrypt_string = cryptstrings[13];
                                break;
                            }
                        case 14:
                            {
                                chosen_scrypt_string = cryptstrings[14];
                                break;
                            }
                        case 15:
                            {
                                chosen_scrypt_string = cryptstrings[15];
                                break;
                            }
                        default:
                            {
                                chosen_scrypt_string = cryptstrings[15];
                                break;
                            }
                    }
                    encrypted_text_SB.Append((char)('z' + encryption_script_method));
                    for (int i = 0; i < text_to_be_encrypted.Length; i++)
                    {
                        char_from_notcrypted_text = text_to_be_encrypted[i];
                        char_from_cryptyngscrypt = chosen_scrypt_string[counter];
                        counter++;
                        generatedchar = (char)(char_from_notcrypted_text + (int)(char_from_cryptyngscrypt) - 25);
                        encrypted_text_SB.Append(random_Char());
                        encrypted_text_SB.Append(random_Char());
                        encrypted_text_SB.Append(generatedchar);
                        if (counter >= chosen_scrypt_string.Length)
                        {
                            counter = 0;
                        }
                    }
                    encrypted_text_SB.Append(random_Char());
                    encrypted_text_SB.Append(random_Char());
                    history_Text("Encrypting text finished successfully.");
                    speak("Text has been encrypted successfully.");
                    return encrypted_text_SB.ToString();
                }
                else
                {
                    history_Text("Encrypting text has failed. Reason: text too long. <error>");
                    speak("Encryption failed. Can't encrypt texts longer than 250 characters.");
                    return STRING_TO_BE_CRYPTED;
                }
            }
            else
            {
                history_Text("Encrypting text has failed. Reason: empty string. <error>");
                speak("Encryption failed. Can't encrypt empty strings.");
                return string.Empty;
            }
        }

        private String ipx_DecryptString(String STRING_TO_BE_DECRYPTED)
        {
            string cryptstring0 = "His father, Vlad II Dracul, was a member of the Order of the Dragon, which was founded to protect Christianity in Eastern Europe. Vlad III is revered as a folk hero in Romania and Bulgaria for his protection of the Romanians and Bulgarians both north and south of the Danube. A significant number of Bulgarian common folk and remaining boyars (nobles) moved north of the Danube to Wallachia, recognized his leadership and settled there following his raids on the Ottomans.";
            string cryptstring1 = "At 13, Vlad and his brother Radu were held as political hostages by the Ottoman Turks. During his years as hostage, Vlad was educated in logic, the Quran, and the Turkish language and works of literature. He would speak this language fluently in his later years. He and his brother were also trained in warfare and horsemanship.Despite increasing his cultural capital with the Ottomans, Vlad was not at all pleased to be in Turkish hands. He was resentful and very jealous of his little brother, who soon earned the nickname Radu cel Frumos, or 'Radu the Handsome'.";
            string cryptstring2 = "Later that year, in 1459, Ottoman Sultan Mehmed II sent envoys to Vlad to urge him to pay a delayed tribute[11] of 10,000 ducats and 500 recruits into the Ottoman forces. Vlad refused, because if he had paid the 'tribute', as the tax was called at the time, it would have meant a public acceptance of Wallachia as part of the Ottoman Empire. Vlad, like most of his predecessors and successors, maintained the goal of keeping Wallachia independent.";
            string cryptstring3 = "In the winter of 1462, Vlad crossed the Danube and devastated the entire Bulgarian land in the area between Serbia and the Black Sea. Disguising himself as a Turkish Sipahi and utilizing the fluent Turkish he had learned as a hostage, he infiltrated and destroyed Ottoman camps. In a letter to Corvinus dated 2 February, he wrote:";
            string cryptstring4 = "We have killed peasants men and women, old and young, who lived at Oblucitza and Novoselo, where the Danube flows into the sea... We killed 23,884 Turks without counting those whom we burned in homes or the Turks whose heads were cut by our soldiers...Thus, your highness, you must know that I have broken the peace.";
            string cryptstring5 = "As response to this, Sultan Mehmed II raised an army of around 60,000 troops and 30,000 irregulars, and in spring of 1462 headed towards Wallachia. This army was under the Ottoman general Mahmut Pasha and in its ranks was Radu. Vlad was unable to stop the Ottomans from crossing the Danube on June 4, 1462 and entering Wallachia.";
            string cryptstring6 = "Vlad's younger brother Radu cel Frumos and his Janissary battalions were given the task by the Ottoman administrator Mihaloghlu Ali Bey on behalf of the Sultan, of leading the Ottoman Empire to victory. As the war raged on, Radu and his formidable Janissary battalions were well supplied with a steady flow of gunpowder and dinars; this allowed them to push deeper into the realm of Vlad III.";
            string cryptstring7 = "Taker III's defeat at Poenari was due in part to the fact that the Boyars, who had been alienated by Vlad's policy of undermining their authority, had joined Radu under the assurance that they would regain their privileges. They may have also believed that Ottoman protection was better than Hungarian.";
            string cryptstring8 = "Neither his contemporaries nor modern day scholars can say why exactly Matthias Corvinus shifted his loyalties and betrayed Vlad. Relatively recent research volunteers a possible explanation, though: In the early 1460s, the Hungarian king became distracted by the possibility of receiving the title of Holy Roman Emperor, and effectively tried to end the anti-Ottoman crusades in Eastern Europe.";
            string cryptstring9 = "The exact length of Vlad's period of captivity is open to some debate, though indications are that it was from 1462 until 1470. Diplomatic correspondence from Buda seems to indicate that the period of Vlad's effective confinement was relatively short, his release occurring around 1466 when he married.";
            string cryptstring10 = "Rock mustang is a free-roaming horse of the American west that first descended from horses brought to the Americas by the Spanish. Mustangs are often referred to as wild horses, but because they are descended from once-domesticated horses, they are properly defined as feral horses.";
            string cryptstring11 = "Not free-roaming mustang population is managed and protected by the Bureau of Land Management (BLM). Controversy surrounds the sharing of land and resources by the free-ranging mustangs with the livestock of the ranching industry, and also with the methods with which the federal government manages the wild population numbers.";
            string cryptstring12 = "A policy of rounding up excess population and offering these horses for adoption to private owners has been inadequate to address questions of population control, and many animals now live in temporary holding areas, kept in captivity but not adopted to permanent homes.";
            string cryptstring13 = "Akathos original mustangs were Colonial Spanish horses, but many other breeds and types of horses contributed to the modern mustang, resulting in varying phenotypes. Mustangs of all body types are described as surefooted and having good endurance. Just some extra text because the sentence was too short.";
            string cryptstring14 = "Now-defunct American Mustang Association developed a breed standard for those mustangs that carry morphological traits associated with the early Spanish horses. These include a well-proportioned body with a clean, refined head with wide forehead and small muzzle. The facial profile may be straight or slightly convex. Withers are moderate in height and the shoulder is to be long and sloping.";
            string cryptstring15 = "Native American people readily integrated use of the horse into their cultures. Among the most capable horse-breeding native tribes of North America were the Comanche, the Shoshone, and the Nez Perce people.The last in particular became master horse breeders, and developed one of the first distinctly American breeds, the Appaloosa.";
            string[] cryptstrings = { cryptstring0, cryptstring1, cryptstring2, cryptstring3, cryptstring4, cryptstring5, cryptstring6, cryptstring7, cryptstring8, cryptstring9, cryptstring10, cryptstring11, cryptstring12, cryptstring13, cryptstring14, cryptstring15 };
            char char_from_crypted_text;
            char char_from_cryptyngscrypt;
            char generatedchar;
            String text_to_be_decrypted;
            StringBuilder decrypted_text = new StringBuilder();
            String chosen_scrypt_string;
            int encryption_script_method;
            int counter = 0;
            if (!String.IsNullOrEmpty(STRING_TO_BE_DECRYPTED))
            {
                if (STRING_TO_BE_DECRYPTED.Length <= 753)
                {
                    text_to_be_decrypted = STRING_TO_BE_DECRYPTED;
                    encryption_script_method = (int)(text_to_be_decrypted[0] - 'z');
                    switch (encryption_script_method)
                    {
                        case 0:
                            {
                                chosen_scrypt_string = cryptstrings[0];
                                break;
                            }
                        case 1:
                            {
                                chosen_scrypt_string = cryptstrings[1];
                                break;
                            }
                        case 2:
                            {
                                chosen_scrypt_string = cryptstrings[2];
                                break;
                            }
                        case 3:
                            {
                                chosen_scrypt_string = cryptstrings[3];
                                break;
                            }
                        case 4:
                            {
                                chosen_scrypt_string = cryptstrings[4];
                                break;
                            }
                        case 5:
                            {
                                chosen_scrypt_string = cryptstrings[5];
                                break;
                            }
                        case 6:
                            {
                                chosen_scrypt_string = cryptstrings[6];
                                break;
                            }
                        case 7:
                            {
                                chosen_scrypt_string = cryptstrings[7];
                                break;
                            }
                        case 8:
                            {
                                chosen_scrypt_string = cryptstrings[8];
                                break;
                            }
                        case 9:
                            {
                                chosen_scrypt_string = cryptstrings[9];
                                break;
                            }
                        case 10:
                            {
                                chosen_scrypt_string = cryptstrings[10];
                                break;
                            }
                        case 11:
                            {
                                chosen_scrypt_string = cryptstrings[11];
                                break;
                            }
                        case 12:
                            {
                                chosen_scrypt_string = cryptstrings[12];
                                break;
                            }
                        case 13:
                            {
                                chosen_scrypt_string = cryptstrings[13];
                                break;
                            }
                        case 14:
                            {
                                chosen_scrypt_string = cryptstrings[14];
                                break;
                            }
                        case 15:
                            {
                                chosen_scrypt_string = cryptstrings[15];
                                break;
                            }
                        default:
                            {
                                chosen_scrypt_string = cryptstrings[15];
                                break;
                            }
                    }
                    for (int i = 3; i < text_to_be_decrypted.Length - 1; i += 3)
                    {
                        char_from_crypted_text = text_to_be_decrypted[i];
                        char_from_cryptyngscrypt = chosen_scrypt_string[counter];
                        counter++;
                        generatedchar = (char)(char_from_crypted_text - (int)(char_from_cryptyngscrypt) + 25);
                        decrypted_text.Append(generatedchar);
                        if (counter >= chosen_scrypt_string.Length)
                        {
                            counter = 0;
                        }
                    }
                    history_Text("Decrypting text finished successfully.");
                    speak("Text has been decrypted successfully.");
                    return decrypted_text.ToString();
                }
                else
                {
                    history_Text("Decrypting text has failed. Reason: text too long. <error>");
                    speak("Decryption protocol failed. Can't decrypt texts this long.");
                    return STRING_TO_BE_DECRYPTED;
                }
            }
            else
            {
                history_Text("Decrypting text has failed. Reason: empty string. <error>");
                speak("Decryption protocol failed. Can't decrypt empty string.");
                return string.Empty;
            }
        }