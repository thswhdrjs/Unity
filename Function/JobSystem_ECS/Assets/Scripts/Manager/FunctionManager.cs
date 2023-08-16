using System;
using System.Collections;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FunctionManager : Singleton<FunctionManager>
{
    // Set DontDestroyOnLoad
    public void SetDontDestroy(GameObject obj)
    {
        obj.transform.parent = null;
        DontDestroyOnLoad(obj);
    }

    // obj의 자식들 SetActive(isActive)
    public void InitChild(GameObject obj, bool isActive)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
            obj.transform.GetChild(i).gameObject.SetActive(isActive);
    }

    // 배열의 SetActive(isActive)
    public void InitChild(GameObject[] objArray, bool isActive)
    {
        foreach (GameObject obj in objArray)
            obj.SetActive(isActive);
    }

    // Clipping
    public IEnumerator Clipping(GameObject obj)
    {
        Material mt = obj.GetComponent<MeshRenderer>().sharedMaterial;

        if (mt.shader != Shader.Find("Shader Graphs/Clipping"))
            yield break;

        float currFlow = mt.GetFloat("Vector1_173744b7f29144a4bd3d966c9813a2e4");
        float targetFlow = currFlow == -1f ? 2f : -1f;
        int flag = currFlow == -1f ? 1 : -1;

        while (-1f <= currFlow && currFlow <= 2f)
        {
            currFlow += Time.deltaTime * flag;
            mt.SetFloat("Vector1_173744b7f29144a4bd3d966c9813a2e4", currFlow);
            yield return new WaitForEndOfFrame();
        }

        mt.SetFloat("Vector1_173744b7f29144a4bd3d966c9813a2e4", targetFlow);
    }

    // obj targetPos까지 이동
    public IEnumerator Move(GameObject obj, Vector3 targetPos, float cost, bool isLocal, bool check)
    {
        if (obj == null)
            yield break;

        Vector3 currPos = isLocal ? obj.transform.localPosition : obj.transform.position;
        float speed = GetDistance(currPos, targetPos) * Time.deltaTime / cost;
        float num = 0.001f;

        bool isEuqalX = false;
        bool isEuqalY = false;
        bool isEuqalZ = false;

        while (!isEuqalX || !isEuqalY | !isEuqalZ)
        {
            if (GameManager.whileCheck)
            {
                GameManager.whileCheck = false;
                yield break;
            }

            yield return new WaitForEndOfFrame();
            currPos = isLocal ? obj.transform.localPosition : obj.transform.position;

            isEuqalX = currPos.x <= targetPos.x ? currPos.x + num >= targetPos.x : currPos.x - num <= targetPos.x;
            isEuqalY = currPos.y <= targetPos.y ? currPos.y + num >= targetPos.y : currPos.y - num <= targetPos.y;
            isEuqalZ = currPos.z <= targetPos.z ? currPos.z + num >= targetPos.z : currPos.z - num <= targetPos.z;

            if (isLocal)
                obj.transform.localPosition = Vector3.MoveTowards(currPos, targetPos, speed);
            else
                obj.transform.position = Vector3.MoveTowards(currPos, targetPos, speed);
        }

        if (isLocal)
            obj.transform.localPosition = targetPos;
        else
            obj.transform.position = targetPos;
    }

    #region SceneManager

    // Get Current Scene Name
    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    // Get All Scene Name
    public string[] GetAllSceneName()
    {
        string[] sceneName = new string[SceneManager.sceneCountInBuildSettings];

        for (int i = 0; i < sceneName.Length; i++)
            sceneName[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

        return sceneName;
    }

    // Load Scene By Index
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    // Load Scene By Name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    #endregion

    #region Test

    // work 코드 실행 속도
    public void RunningTime(Action work)
    {
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        work();
        sw.Stop();
        print(sw.ElapsedMilliseconds.ToString() + "ms");
    }

    public void Print(string[] arr)
    {
        string result = arr[0];

        for (int i = 1; i < arr.Length; i++)
            result += " / " + arr[i];

        print(result);
    }

    public void CubeTest(bool condition, Color rightColor, Color wrongColor)
    {
        GameObject.Find("Cube").GetComponent<MeshRenderer>().sharedMaterial.color = condition ? rightColor : wrongColor;
    }

    public void TextTest(string message)
    {
        GameObject.Find("Text").GetComponent<TextMeshPro>().text += "/" + message;
    }

    #endregion

    #region System

    // Frame 고정
    public void SetFrameRate(int targetFrame)
    {
        Application.targetFrameRate = targetFrame;
    }

    // 해상도 고정
    public void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, true);
    }

    // 다중 디스플레이
    public void MultiDisplay()
    {
        if (Display.displays.Length == 1)
            return;

        foreach (var item in Display.displays)
            item.Activate();
    }

    #endregion

    #region UI

    // Animation 무한 재생
    public void AnimationRestart()
    {
        bool isEndAnimation = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;

        if (isEndAnimation)
        {
            GetComponent<Animator>().Rebind();
            GetComponent<Animator>().enabled = false;
            GetComponent<Animator>().enabled = true;
        }
    }

    #region Dropdown

    // Dropdown에 keyword 존재 확인
    public bool IsDuplicateDropdown(Dropdown.OptionData data, string keyword)
    {
        return data.text.Equals(keyword) ? true : false;
    }

    // 현재 선택된 Dropdown 값 반환
    public string GetDropdownValue(GameObject obj)
    {
        return obj.GetComponent<Dropdown>().options[obj.GetComponent<Dropdown>().value].text;
    }

    #endregion

    #endregion

    #region Math

    // digit 자리수에서 반올림
    public float Round(float num, int digit)
    {
        float cipher = Mathf.Pow(10, digit);
        return Mathf.Round(num * cipher) / cipher;
    }

    // start ~ end 사이의 직선 거리
    public float GetDistance(Vector3 start, Vector3 end)
    {
        return Mathf.Sqrt(Mathf.Pow(end.x - start.x, 2) + Mathf.Pow(end.z - start.z, 2));
    }

    // weight 3자리 마다 , 추가
    public string GetDigit(string weight)
    {
        int start = 0, end = weight.Length % 3 == 0 ? 3 : weight.Length % 3;
        string result = weight.Substring(0, end);

        if (weight.Length < 4)
            return weight;

        while (weight.Length > start + end)
        {
            start = start + end;
            end = 3;
            result += "," + weight.Substring(start, end);
        }

        return result;
    }

    // obj ~ targetObj까지 포물선
    public void Parabola(GameObject obj, GameObject targetObj)
    {
        LineRenderer lr = obj.GetComponent<LineRenderer>() == null ? obj.AddComponent<LineRenderer>() : obj.GetComponent<LineRenderer>();

        Vector3 startPos = obj.transform.position;
        Vector3 endPos = targetObj.transform.position;
        Vector3 center = (startPos + endPos) * 0.5f;
        center.y -= 3;

        startPos -= center;
        endPos -= center;

        for (int i = 0; i < lr.positionCount; i++)
        {
            Vector3 point = Vector3.Slerp(startPos, endPos, i / (float)(lr.positionCount - 1));
            point += center;
            lr.SetPosition(i, point);
        }
    }

    #region Max

    // value, value2 중 큰 수 반환
    public float GetMax(float value, float value2)
    {
        return value >= value2 ? value : value2;
    }

    // value, value2, value3 중 큰 수 반환
    public float GetMax(float value, float value2, float value3)
    {
        float max = value;

        if (max < value2)
            max = value2;

        if (max < value3)
            max = value3;

        return max;
    }

    // value, value2, value3 중 큰 수 반환
    public float GetMax<T>(T[] data)
    {
        Type type = data.GetType();
        float max = float.MinValue;

        for (int i = 0; i < data.Length; i++)
        {
            float temp = type.ToString().Equals("System.Int32[]") ? (float)(data[i] as int?) : (float)(data[i] as float?);

            if (max < temp)
                max = temp;
        }

        return max;
    }

    #endregion

    #region Min

    // value, value2 중 작은 수 반환
    public float GetMin(float value, float value2)
    {
        float min = value;

        if (min > value2)
            min = value2;

        return value >= value2 ? value2 : value;
    }

    // value, value2, value3 중 작은 수 반환
    public float GetMin(float value, float value2, float value3)
    {
        float min = value;

        if (min > value2)
            min = value2;

        if (min > value3)
            min = value3;

        return min;
    }

    // value, value2, value3 중 작은 수 반환
    public float GetMin<T>(T[] data)
    {
        Type type = data.GetType();
        float max = float.MaxValue;

        for (int i = 0; i < data.Length; i++)
        {
            float temp = type.ToString().Equals("System.Int32[]") ? (float)(data[i] as int?) : (float)(data[i] as float?);

            if (max > temp)
                max = temp;
        }

        return max;
    }

    #endregion

    #region Sort

    // comare > compare2 => -1, comare = compare2 => 0, comare < compare2 => 1
    public int Compare<T>(T compare, T compare2)
    {
        float num = float.Parse(compare.ToString());
        float num2 = float.Parse(compare2.ToString());

        if (num == num2)
            return 0;

        return num > num2 ? -1 : 1;
    }

    // arr 배열의 arr[n] <-> arr[n2]
    public void Swap<T>(T[] arr, int n, int n2)
    {
        T temp = arr[n];
        arr[n] = arr[n2];
        arr[n2] = temp;
    }

    // arr 배열의 중간 값 반환(start, center, end 비교)
    public int GetPivot<T>(T[] arr, int start, int end)
    {
        int center = (start + end) / 2;

        if (Compare(arr[start], arr[center]) == -1)
            Swap(arr, start, center);

        if (Compare(arr[start], arr[end]) == -1)
            Swap(arr, start, end);

        if (Compare(arr[center], arr[end]) == -1)
            Swap(arr, center, end);

        return center;
    }

    // arr 배열 오름차순으로 정렬
    public void QuickSort<T>(T[] arr, int start, int end)
    {
        int pivot = GetPivot(arr, start, end);
        int i = start, j = end;

        while (i <= j)
        {
            while (Compare(arr[i], arr[pivot]) == 1)
                i++;

            while (Compare(arr[j], arr[pivot]) == -1)
                j--;

            if (i <= j)
                Swap(arr, i++, j--);
        }

        if (start < j)
            QuickSort(arr, start, j);

        if (end > i)
            QuickSort(arr, i, end);
    }

    public void Merge<T>(T[] arr, int start, int pivot, int end)
    {
        T[] tempArr = new T[arr.Length];
        Array.Copy(arr, tempArr, tempArr.Length);

        for (int i = 0; i < tempArr.Length; i++)
            tempArr[i] = arr[i];

        int p = start;
        int q = pivot + 1;
        int index = 0;

        while (p <= pivot && q <= end)
            tempArr[index++] = Compare(arr[p], arr[q]) > -1 ? arr[p++] : arr[q++];

        while (p <= pivot)
            tempArr[index++] = arr[p++];

        while (q <= end)
            tempArr[index++] = arr[q++];

        p = start;
        index = 0;

        while (p <= end)
            arr[p++] = tempArr[index++];
    }

    public void MergeSort<T>(T[] arr, int start, int end)
    {
        if (start >= end)
            return;

        int pivot = (start + end) / 2;

        MergeSort(arr, start, pivot);
        MergeSort(arr, pivot + 1, end);
        Merge(arr, start, pivot, end);
    }

    #endregion

    #endregion

    #region Fade

    // Fade Out ~ Fade In
    public IEnumerator Fade()
    {
        Singleton.Instance.loading.SetActive(true);
        Singleton.Instance.imageLoading.SetActive(true);

        Image fadeImg = Singleton.Instance.imageLoading.GetComponent<Image>();
        fadeImg.color = new Color(0.2235294f, 0.2392157f, 0.2431373f, 0);

        Color color = fadeImg.color;

        while (0f <= color.a && color.a <= 1f)
        {
            color.a += Time.deltaTime * 1.6f;
            fadeImg.color = color;
            yield return new WaitForEndOfFrame();
        }

        color.a = 1f;
        fadeImg.color = color;
        yield return new WaitForSeconds(1f);

        while (0f <= color.a && color.a <= 1f)
        {
            color.a -= Time.deltaTime * 1.6f;
            fadeImg.color = color;
            yield return new WaitForEndOfFrame();
        }

        color.a = 0f;
        fadeImg.color = color;
    }

    // Fade Out ~ fadeAction ~ Fade In
    public IEnumerator Fade(Action fadeAction)
    {
        Singleton.Instance.loading.SetActive(true);
        Singleton.Instance.imageLoading.SetActive(true);

        Image fadeImg = Singleton.Instance.imageLoading.GetComponent<Image>();
        fadeImg.color = new Color(0.2235294f, 0.2392157f, 0.2431373f, 0);

        Color color = fadeImg.color;

        while (0f <= color.a && color.a <= 1f)
        {
            color.a += Time.deltaTime * 1.6f;
            fadeImg.color = color;
            yield return new WaitForEndOfFrame();
        }

        color.a = 1f;
        fadeImg.color = color;

        yield return new WaitForEndOfFrame();
        fadeAction();

        yield return new WaitForSeconds(1f);

        while (0f <= color.a && color.a <= 1f)
        {
            color.a -= Time.deltaTime * 1.6f;
            fadeImg.color = color;
            yield return new WaitForEndOfFrame();
        }

        color.a = 0f;
        fadeImg.color = color;
    }

    // Fade In Or Fade Out
    public IEnumerator Fade(bool isFadeIn)
    {
        Singleton.Instance.loading.SetActive(true);
        Singleton.Instance.imageLoading.SetActive(true);

        float setAlpha = isFadeIn ? 1f : 0f;
        float targetAlpha = isFadeIn ? 0f : 1f;
        float flag = isFadeIn ? -1f : 1f;

        Image fadeImg = Singleton.Instance.imageLoading.GetComponent<Image>();
        fadeImg.color = new Color(0.2235294f, 0.2392157f, 0.2431373f, setAlpha);

        Color color = fadeImg.color;

        while (0f <= color.a && color.a <= 1f)
        {
            color.a += Time.deltaTime * 1.6f * flag;
            fadeImg.color = color;
            yield return new WaitForEndOfFrame();
            print(1);
        }

        color.a = targetAlpha;
        fadeImg.color = color;

        yield return new WaitForEndOfFrame();
    }

    // Fade In Or Fade Out ~ fadeAction
    public IEnumerator Fade(bool isFadeIn, Action fadeAction)
    {
        Singleton.Instance.loading.SetActive(true);
        Singleton.Instance.imageLoading.SetActive(true);

        float setAlpha = isFadeIn ? 1f : 0f;
        float targetAlpha = isFadeIn ? 0f : 1f;
        float flag = isFadeIn ? -1f : 1f;

        Image fadeImg = Singleton.Instance.imageLoading.GetComponent<Image>();
        fadeImg.color = new Color(0.2235294f, 0.2392157f, 0.2431373f, setAlpha);

        Color color = fadeImg.color;

        while (0f <= color.a && color.a <= 1f)
        {
            color.a += Time.deltaTime * 1.6f * flag;
            fadeImg.color = color;
            yield return new WaitForEndOfFrame();
        }

        color.a = targetAlpha;
        fadeImg.color = color;

        yield return new WaitForEndOfFrame();
        fadeAction();
    }

    // obj Fade In ~ Fade Out
    public IEnumerator FadeObject(GameObject obj)
    {
        Material fadeMt = obj.GetComponent<MeshRenderer>().sharedMaterial;
        fadeMt.SetFloat("_Opacity", 1);

        float opacity = 1f;

        while (0f <= opacity && opacity <= 1f)
        {
            opacity -= Time.deltaTime * 1.6f;
            fadeMt.SetFloat("_Opacity", opacity);
            yield return new WaitForEndOfFrame();
        }

        opacity = 0f;
        fadeMt.SetFloat("_Opacity", opacity);

        yield return new WaitForSeconds(1f);

        while (0f <= opacity && opacity <= 1f)
        {
            opacity += Time.deltaTime * 1.6f;
            fadeMt.SetFloat("_Opacity", opacity);
            yield return new WaitForEndOfFrame();
        }

        opacity = 1f;
        fadeMt.SetFloat("_Opacity", opacity);
    }

    #endregion

    #region Array

    /*******************************/
    /**** Array 함수 적극 활용! ****/
    /*******************************/

    // arr Reverse
    public T[] Reverse<T>(T[] arr)
    {
        T[] temp = new T[arr.Length];
        int index = 0;

        for (int i = temp.Length - 1; i > -1; i--)
        {
            temp[index] = arr[i];
            index++;
        }

        return temp;
    }

    // arr 안 중복 요소 제거 후 반환
    public T[] Deduplicate<T>(T[] arr)
    {
        switch (arr.GetType().ToString())
        {
            case "System.Int32[]":
                {
                    int[] compareArr = new int[arr.Length];

                    for (int i = 0; i < compareArr.Length; i++)
                        compareArr[i] = int.Parse(arr[i].ToString());

                    int[] deduplicationArr = new int[arr.Length];
                    int index = 0;

                    for (int i = 0; i < deduplicationArr.Length; i++)
                        deduplicationArr[i] = int.MinValue;

                    for (int i = 0; i < deduplicationArr.Length; i++)
                    {
                        bool isExist = false;

                        for (int j = 0; j < deduplicationArr.Length; j++)
                        {
                            if (deduplicationArr[j] != compareArr[i])
                                continue;

                            isExist = true;
                            break;
                        }

                        if (!isExist)
                        {
                            deduplicationArr[index] = compareArr[i];
                            index++;
                        }
                    }

                    compareArr = new int[index];

                    for (int i = 0; i < compareArr.Length; i++)
                        compareArr[i] = deduplicationArr[i];

                    T[] result = new T[compareArr.Length];
                    Array.Copy(compareArr, result, compareArr.Length);
                    return result;
                }
            case "System.Single[]":
                {
                    float[] compareArr = new float[arr.Length];

                    for (int i = 0; i < compareArr.Length; i++)
                        compareArr[i] = float.Parse(arr[i].ToString());

                    float[] deduplicationArr = new float[arr.Length];
                    int index = 0;

                    for (int i = 0; i < deduplicationArr.Length; i++)
                        deduplicationArr[i] = float.MinValue;

                    for (int i = 0; i < deduplicationArr.Length; i++)
                    {
                        bool isExist = false;

                        for (int j = 0; j < deduplicationArr.Length; j++)
                        {
                            if (deduplicationArr[j] != compareArr[i])
                                continue;

                            isExist = true;
                            break;
                        }

                        if (!isExist)
                        {
                            deduplicationArr[index] = compareArr[i];
                            index++;
                        }
                    }

                    compareArr = new float[index];

                    for (int i = 0; i < compareArr.Length; i++)
                        compareArr[i] = deduplicationArr[i];

                    T[] result = new T[compareArr.Length];
                    Array.Copy(compareArr, result, compareArr.Length);
                    return result;
                }
            case "System.String[]":
                {
                    string[] compareArr = new string[arr.Length];

                    for (int i = 0; i < compareArr.Length; i++)
                        compareArr[i] = arr[i].ToString();

                    string[] deduplicationArr = new string[arr.Length];
                    int index = 0;

                    for (int i = 0; i < deduplicationArr.Length; i++)
                        deduplicationArr[i] = string.Empty;

                    for (int i = 0; i < deduplicationArr.Length; i++)
                    {
                        bool isExist = false;

                        for (int j = 0; j < deduplicationArr.Length; j++)
                        {
                            if (deduplicationArr[j] != compareArr[i])
                                continue;

                            isExist = true;
                            break;
                        }

                        if (!isExist)
                        {
                            deduplicationArr[index] = compareArr[i];
                            index++;
                        }
                    }

                    compareArr = new string[index];

                    for (int i = 0; i < compareArr.Length; i++)
                        compareArr[i] = deduplicationArr[i];

                    T[] result = new T[compareArr.Length];
                    Array.Copy(compareArr, result, compareArr.Length);
                    return result;
                }
        }

        return arr;
    }

    // arr 안 keyword 존재 확인해 처음으로 발견한 index 반환 없으면 -1 반환
    public int GetIndex<T>(T[] arr, T keyword)
    {
        switch (keyword.GetType().ToString())
        {
            case "System.Int32":
                {
                    int[] compareArr = new int[arr.Length];

                    for (int i = 0; i < compareArr.Length; i++)
                        compareArr[i] = int.Parse(arr[i].ToString());

                    int compare = int.Parse(keyword.ToString());

                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (compareArr[i] == compare)
                            return i;
                    }

                    break;
                }
            case "System.Single":
                {
                    float[] compareArr = new float[arr.Length];

                    for (int i = 0; i < compareArr.Length; i++)
                        compareArr[i] = float.Parse(arr[i].ToString());

                    float compare = float.Parse(keyword.ToString());

                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (compareArr[i] == compare)
                            return i;
                    }

                    break;
                }
            case "System.String":
                {
                    string[] compareArr = new string[arr.Length];

                    for (int i = 0; i < compareArr.Length; i++)
                        compareArr[i] = arr[i].ToString();

                    string compare = keyword.ToString();

                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (compareArr[i].Equals(compare))
                            return i;
                    }

                    break;
                }
        }

        return -1;
    }

    // Class T의 keyword 변수명의 할당 값 반환
    public T GetCompareName<T>(T compare, string keyword)
    {
        Type type = compare.GetType();
        BindingFlags bf = BindingFlags.Public | BindingFlags.Instance;
        FieldInfo fieldInfo = type.GetField(keyword, bf);

        return (T)fieldInfo.GetValue(compare);
    }

    // Calss T의 compareName 존재 확인
    public int GetStringArrIndex<T>(T[] compare, string name, string keyword)
    {
        for (int i = 0; i < compare.Length; i++)
        {
            if (name.Equals(GetCompareName(compare[i], keyword)))
                return i;
        }

        return -1;
    }

    // Class T 관련 Drodown에서 compareName 존재 확인
    public void SetDropdown<T>(T[] compare, GameObject[] compareObj, GameObject dropdownObj, string compareName, string compareElement)
    {
        Dropdown dd = dropdownObj.GetComponent<Dropdown>();

        if (dd.options.Count > 0)
        {
            foreach (Dropdown.OptionData data in dd.options)
            {
                bool isExist = false;

                for (int i = 0; i < compare.Length; i++)
                {
                    if (compareObj[i] == null || compare[i] == null)
                        continue;

                    if (GetCompareName(compare[i], compareElement) == null)
                        continue;

                    if (GetCompareName(compare[i], compareElement).Equals(""))
                        continue;

                    if (data.text.Equals(GetCompareName(compare[i], compareName)))
                    {
                        isExist = true;
                        break;
                    }
                }

                if (!isExist)
                {
                    dd.options.Remove(data);
                    break;
                }
            }
        }

        for (int i = 0; i < compare.Length; i++)
        {
            if (compareObj[i] == null || compare[i] == null)
                continue;

            if (GetCompareName(compare[i], compareElement) == null)
                continue;

            if (GetCompareName(compare[i], compareElement).Equals(""))
                continue;

            bool isExist = false;

            foreach (Dropdown.OptionData data in dd.options)
            {
                if (GetCompareName(compare[i], compareName).Equals(data.text))
                {
                    isExist = true;
                    break;
                }
            }

            if (!isExist)
                dd.options.Add(new Dropdown.OptionData(GetCompareName(compare[i].ToString(), compareName), null));
        }
    }

    #endregion

    #region string

    // Reverse
    public string Reverse(string s)
    {
        string reverse = string.Empty;

        for (int i = s.Length - 1; i >= 0; i--)
            reverse += s[i].ToString();

        return reverse;
    }

    // s에서 re 문자열 검색 후 존재하면 처음 시작하는 index 반환, 존재하지 않으면 -1 반환
    public int GetIndex(string s, string re)
    {
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i].Equals(re[0]))
            {
                bool isEqual = true;

                for (int j = 0; j < re.Length; j++)
                {
                    if (!s[i + j].Equals(re[j]))
                        isEqual = false;
                }

                if (isEqual)
                    return i;
            }
        }

        return -1;
    }

    // s에서 첫 re만 ch로 변환
    public string Replace(string s, string re, string ch)
    {
        int index = GetIndex(s, re);

        if (index == -1)
            return s;

        return s.Substring(0, index) + ch + s.Substring(index + re.Length, s.Length - index - re.Length);
    }

    // 긴 숫자 string 각 자리를 string 배열로 반환 ex) 5886452358741 => "5886452358741"
    public string[] GetEachDigitStringArray(string s, int stringLength)
    {
        string[] num = new string[stringLength];
        int index = num.Length - s.Length;

        for (int i = 0; i < index; i++)
            num[i] = "0";

        for (int i = index; i < num.Length; i++)
            num[i] = s[i - index].ToString();

        return num;
    }

    // string 배열을 하나의 string으로 반환
    public string GetCombineByArray(string[] str)
    {
        string result = string.Empty;

        foreach (string s in str)
            result += s;

        return result;
    }

    #endregion

    #region FixedJoint

    // Start FixedJoint
    public void StartFixedJoint(GameObject obj)
    {
        if (GetComponent<FixedJoint>() == null)
            gameObject.AddComponent<FixedJoint>();

        if (obj.GetComponent<Rigidbody>() == null)
            obj.AddComponent<Rigidbody>().useGravity = false;

        FixedJoint fj = GetComponent<FixedJoint>();
        fj.connectedBody = obj.GetComponent<Rigidbody>();
    }

    // End FixedJoint
    public void EndFixedJoint(GameObject obj)
    {
        if (GetComponent<FixedJoint>() != null)
            Destroy(GetComponent<FixedJoint>());

        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }

        if (obj.GetComponent<Rigidbody>() != null)
        {
            obj.GetComponent<Rigidbody>().isKinematic = true;
            obj.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    #endregion

    #region Raycast

    // Raycast
    public void BasicRaycast()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            Debug.Log("hit point : " + hit.point + ", distance : " + hit.distance + ", name : " + hit.collider.name);
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
        }
        else
            Debug.DrawRay(transform.position, transform.forward * 1000f, Color.red);
    }

    // maxDistance 만큼 충돌 확인 Raycast
    public void MaxDistanceRaycast(float maxDistance)
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance))
        {
            Debug.Log("hit point : " + hit.point + ", distance : " + hit.distance + ", name : " + hit.collider.name);
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
        }
        else
            Debug.DrawRay(transform.position, transform.forward * 1000f, Color.red);
    }

    // 해당 layerIndex만 충돌 확인 Raycast
    public void LayerMaskRaycast(float maxDistance, int layerIndex)
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance, layerIndex))
        {
            Debug.Log("hit point : " + hit.point + ", distance : " + hit.distance + ", name : " + hit.collider.name);
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
        }
        else
            Debug.DrawRay(transform.position, transform.forward * 1000f, Color.red);
    }

    #endregion

    #region Time Format

    // YYYY-MM-DD 오전/오후 hh:mm:ss to YYYYMMDDhhmmss
    public string GetText(string time)
    {
        bool isPM = false;
        time = time.Replace("-", "");
        time = time.Replace(":", "");
        time = time.Replace(" ", "");

        if (time.Contains("오전"))
            time = time.Replace("오전", "");

        if (time.Contains("오후"))
        {
            time = time.Replace("오후", "");
            isPM = true;
        }

        if (time.Length == 13)
            time = time.Substring(0, 8) + "0" + time.Substring(8, 5);

        if (isPM && int.Parse(time.Substring(9, 2)) < 10)
            time = time.Substring(0, 8) + (int.Parse(time.Substring(9, 2)) + 12).ToString() + time.Substring(10, 4);

        return time;
    }

    // YYYYMMDDhhmmss to YYYY-MM-DD 오전/오후 hh:mm:ss
    public string SetTimeFormat(string time)
    {
        string year = time.Substring(0, 4);
        string month = time.Substring(4, 2);
        string day = time.Substring(6, 2);

        string hour = time.Substring(8, 2);
        string ampm = int.Parse(hour) > 11 ? "오후" : "오전";
        hour = int.Parse(hour) > 12 ? (int.Parse(hour) - 12).ToString() : hour;
        hour = int.Parse(hour) == 0 ? "12" : hour;

        string minute = time.Substring(10, 2);
        string second = time.Substring(12, 2);
        string remainder = string.Empty;

        if (time.Length > 12)
            remainder = " / " + time.Substring(13, 1);

        return year + "-" + month + "-" + day + " " + ampm + " " + hour + ":" + minute + ":" + second + remainder;
    }

    #endregion
}
