using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiCalculator.Contract
{

    public interface IPiCalcPresenter
    {
        IPiCalcView View { get; set; }

        //1.運用ConcurrentQueue 進行任務的添加(排隊)
        //2.運用ConcurrentDictionary 進行任務添加前的檢查
        //3.運用ConcurrentBag 進行任務完成的回傳(提供給定時器返回結果刷新畫面)

        /// <summary>
        /// 啟動執行緒任務，不斷監聽/接收來自Queue裡面的任務請求，並開始計算
        /// </summary>
        void StartMission();

        /// <summary>
        /// 暫時不實作， 主要用來暫停所有請求接收
        /// </summary>
        void StopMission();


        /// <summary>
        /// 傳入指定的sampleSize並計算PI結果
        /// </summary>
        /// <param name="sampleSize"></param>
        PiMissionModel SendMissionRequest(long sampleSize);

        /// <summary>
        /// 發送任務完成的請求，讓後端將完成的 PiModel Mission 主動回傳給前端
        /// </summary>
        void FetchCompletedMission();


       
       
    }

    public interface IPiCalcView
    {
        IPiCalcPresenter Presenter { get; set; }

        /// <summary>
        /// 當FetchCompletedMission被呼叫時，後端會自動將完成的任務包裝為 List<PiMissionModel>  並呼叫 UpdateDataView 給前端
        /// </summary>
        /// <param name="datas"></param>
        void UpdateDataView(List<PiMissionModel> datas);
    }

}
