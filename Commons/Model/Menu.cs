using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Commons.Cache;
using Commons.WinForm;
namespace Commons.Model
{
    public abstract class Menu
    {
        public string MeunId { get; set; }
        public string MeunName { get; set; }
        public string MeunPic { get; set; }
        public string MeunShortcuts { get; set; }
        public string MeunDll { get; set; }
        public string MeunClass { get; set; }
        public string MeunMethod { get; set; }

        //增加部件方法　　
        public abstract void add(Menu menu);
　　    //删除部件方法
        public abstract bool remove(Menu menu);
　　    //注意这里，这里就提供一种用于访问组合体类的部件方法。
        public abstract Menu getChild(int i);

        public Menu()
        {

        }
        public  Menu(Menu menu)
        {
            this.MeunId = menu.MeunId;
            this.MeunName = menu.MeunName;
            this.MeunPic = menu.MeunPic;
            this.MeunShortcuts = menu.MeunShortcuts;
            this.MeunDll = menu.MeunDll;
            this.MeunClass = menu.MeunClass;
            this.MeunMethod = menu.MeunMethod;
        }
　　
    }
    public class MenuTable : Menu
    {
        public string ParentMeunId { get; set; }
        public int Meunlevel { get; set; }
       
        public override void add(Menu c)
        {
            
        }

        public override bool remove(Menu c)
        {
            //异常处理或错误提示    
            return false;
        }

        public override Menu getChild(int i)
        {
            //异常处理或错误提示   
            return null;
        }  
    }
    public class MenuLeaf : Menu
    {


        public override void add(Menu c)
        {
            
        }

        public override bool remove(Menu c)
        {
            return false; 
        }

        public override Menu getChild(int i)
        {
            //异常处理或错误提示   
            return null;
        }  

    }
    public class MenuComposite : Menu
    {
        private List<Menu> list = new List<Menu>();
        public MenuComposite(Menu menu): base(menu)
        {
            
           
        }
        public override void add(Menu c)
        {
            list.Add(c);
        }

        public override bool remove(Menu c)
        {
            return list.Remove(c);
        }
        public override Menu getChild(int i)
        {
            return (Menu)list[i];
        }  
    }

    public static class MenuWorker
    {
        public static List<MenuTable> GetMenuData()
        {
            return MemoryCacheHelper.GetCacheItem<List<MenuTable>>("Menu",
                delegate() { return MenuWorker.loadMenu(); },
                new TimeSpan(0, 600, 0));//600分钟过期

        }
        public static List<MenuTable> loadMenu()
        {
            #region 菜单
            //List<MenuTable> mtList = new List<MenuTable>();
            //MenuTable mt = new MenuTable();
            //mt.MeunId = "SalesManager";
            //mt.Meunlevel = 0;
            //mt.MeunName = "销售管理";
            //mt.MeunPic = "favorites.png";
            //mt.MeunShortcuts = "";
            //mtList.Add(mt);
            //MenuTable mt1 = new MenuTable();
            //mt1.MeunId = "SalesOrder";
            //mt1.Meunlevel = 1;
            //mt1.MeunName = "销售订单";
            //mt1.MeunPic = "favorites.png";
            //mt1.MeunShortcuts = "01";
            //mt1.ParentMeunId = "SalesManager";
            //mt1.MeunDll = "SalesOrder.dll";
            //mt1.MeunClass = "SalesOrder.Run";
            //mt1.MeunMethod = "Show";
            //mtList.Add(mt1);

            //mt1 = new MenuTable();
            //mt1.MeunId = "SalesOrderQuery";
            //mt1.Meunlevel = 1;
            //mt1.MeunName = "销售订单查询";
            //mt1.MeunPic = "favorites.png";
            //mt1.MeunShortcuts = "02";
            //mt1.ParentMeunId = "SalesManager";
            //mt1.MeunDll = "SalesOrder.dll";
            //mt1.MeunClass = "SalesOrder.Run";
            //mt1.MeunMethod = "ShowQuery";
            //mtList.Add(mt1);

            //mt1 = new MenuTable();
            //mt1.MeunId = "PreOrder";
            //mt1.Meunlevel = 1;
            //mt1.MeunName = "预订单";
            //mt1.MeunPic = "favorites.png";
            //mt1.MeunShortcuts = "02";
            //mt1.ParentMeunId = "SalesManager";
            //mt1.MeunDll = "PreOrder.dll";
            //mt1.MeunClass = "PreOrder.Run";
            //mt1.MeunMethod = "Show";
            //mtList.Add(mt1);

            //mt1 = new MenuTable();
            //mt1.MeunId = "PreOrderQuery";
            //mt1.Meunlevel = 1;
            //mt1.MeunName = "预订单查询";
            //mt1.MeunPic = "favorites.png";
            //mt1.MeunShortcuts = "02";
            //mt1.ParentMeunId = "SalesManager";
            //mt1.MeunDll = "PreOrder.dll";
            //mt1.MeunClass = "PreOrder.Run";
            //mt1.MeunMethod = "ShowQuery";
            //mtList.Add(mt1);

            //MenuTable mt2 = new MenuTable();
            //mt2.MeunId = "StockManager";
            //mt2.Meunlevel = 0;
            //mt2.MeunName = "库存管理";
            //mt2.MeunPic = "favorites.png";
            //mt2.MeunShortcuts = "";
            //mtList.Add(mt2);
            //MenuTable mt3 = new MenuTable();
            //mt3.MeunId = "StockQuery";
            //mt3.Meunlevel = 1;
            //mt3.MeunName = "库存查询";
            //mt3.MeunPic = "favorites.png";
            //mt3.MeunShortcuts = "02";
            //mt3.ParentMeunId = "StockManager";
            //mt3.MeunDll = "Stock.dll";
            //mt3.MeunClass = "Stock.Run";
            //mt3.MeunMethod = "Show";
            //mtList.Add(mt3);

            //MenuTable mt4 = new MenuTable();
            //mt4.MeunId = "SerialQuery";
            //mt4.Meunlevel = 1;
            //mt4.MeunName = "串号查询";
            //mt4.MeunPic = "favorites.png";
            //mt4.MeunShortcuts = "03";
            //mt4.ParentMeunId = "StockManager";
            //mt4.MeunDll = "Stock.dll";
            //mt4.MeunClass = "Stock.Run";
            //mt4.MeunMethod = "SerialShow";
            //mtList.Add(mt4);


            //MenuTable mt5 = new MenuTable();
            //mt5.MeunId = "OrdersManager";
            //mt5.Meunlevel = 0;
            //mt5.MeunName = "指令单";
            //mt5.MeunPic = "favorites.png";
            //mt5.MeunShortcuts = "";
            //mtList.Add(mt5);
            //MenuTable mt6 = new MenuTable();
            //mt6.MeunId = "DeliveryOrder";
            //mt6.Meunlevel = 1;
            //mt6.MeunName = "调拨发货指令单";
            //mt6.MeunPic = "favorites.png";
            //mt6.MeunShortcuts = "04";
            //mt6.ParentMeunId = "OrdersManager";
            //mt6.MeunDll = "GoodsDelivery.dll";
            //mt6.MeunClass = "GoodsDelivery.Run";
            //mt6.MeunMethod = "OrderShow";
            //mtList.Add(mt6);

            //MenuTable mt7 = new MenuTable();
            //mt7.MeunId = "ReceiveOrder";
            //mt7.Meunlevel = 1;
            //mt7.MeunName = "调拨收货指令单";
            //mt7.MeunPic = "favorites.png";
            //mt7.MeunShortcuts = "05";
            //mt7.ParentMeunId = "OrdersManager";
            //mt7.MeunDll = "GoodsReceipt.dll";
            //mt7.MeunClass = "GoodsReceipt.Run";
            //mt7.MeunMethod = "OrderShow";
            //mtList.Add(mt7);


            //MenuTable mt8 = new MenuTable();
            //mt8.MeunId = "Delivery";
            //mt8.Meunlevel = 1;
            //mt8.MeunName = "调拨发货";
            //mt8.MeunPic = "favorites.png";
            //mt8.MeunShortcuts = "06";
            //mt8.ParentMeunId = "StockManager";
            //mt8.MeunDll = "GoodsDelivery.dll";
            //mt8.MeunClass = "GoodsDelivery.Run";
            //mt8.MeunMethod = "Show";
            //mtList.Add(mt8);

            //MenuTable mt9 = new MenuTable();
            //mt9.MeunId = "InventoryOrder";
            //mt9.Meunlevel = 1;
            //mt9.MeunName = "盘点指令单";
            //mt9.MeunPic = "favorites.png";
            //mt9.MeunShortcuts = "07";
            //mt9.ParentMeunId = "OrdersManager";
            //mt9.MeunDll = "Inventory.dll";
            //mt9.MeunClass = "Inventory.Run";
            //mt9.MeunMethod = "OrderShow";
            //mtList.Add(mt9);

            //MenuTable mt10 = new MenuTable();
            //mt10.MeunId = "WarehouseInOrder";
            //mt10.Meunlevel = 1;
            //mt10.MeunName = "入库指令单";
            //mt10.MeunPic = "favorites.png";
            //mt10.MeunShortcuts = "07";
            //mt10.ParentMeunId = "OrdersManager";
            //mt10.MeunDll = "WarehouseIn.dll";
            //mt10.MeunClass = "WarehouseIn.Run";
            //mt10.MeunMethod = "OrderShow";
            //mtList.Add(mt10);

            //MenuTable mt11 = new MenuTable();
            //mt11.MeunId = "WarehouseOutOrder";
            //mt11.Meunlevel = 1;
            //mt11.MeunName = "出库指令单";
            //mt11.MeunPic = "favorites.png";
            //mt11.MeunShortcuts = "07";
            //mt11.ParentMeunId = "OrdersManager";
            //mt11.MeunDll = "WarehouseOut.dll";
            //mt11.MeunClass = "WarehouseOut.Run";
            //mt11.MeunMethod = "OrderShow";
            //mtList.Add(mt11);

            //MenuTable mt12 = new MenuTable();
            //mt12.MeunId = "WarehouseTransferOrder";
            //mt12.Meunlevel = 1;
            //mt12.MeunName = "移库指令单";
            //mt12.MeunPic = "favorites.png";
            //mt12.MeunShortcuts = "08";
            //mt12.ParentMeunId = "OrdersManager";
            //mt12.MeunDll = "WarehouseTransfer.dll";
            //mt12.MeunClass = "WarehouseTransfer.Run";
            //mt12.MeunMethod = "OrderShow";
            //mtList.Add(mt12);

            //MenuTable mt13 = new MenuTable();
            //mt13.MeunId = "PaymentManager";
            //mt13.Meunlevel = 0;
            //mt13.MeunName = "缴款管理";
            //mt13.MeunPic = "favorites.png";
            //mt13.MeunShortcuts = "";
            //mtList.Add(mt13);
            //MenuTable mt14 = new MenuTable();
            //mt14.MeunId = "Payment";
            //mt14.Meunlevel = 1;
            //mt14.MeunName = "缴款";
            //mt14.MeunPic = "favorites.png";
            //mt14.MeunShortcuts = "01";
            //mt14.ParentMeunId = "PaymentManager";
            //mt14.MeunDll = "Payment.dll";
            //mt14.MeunClass = "Payment.Run";
            //mt14.MeunMethod = "Show";
            //mtList.Add(mt14);

            //MenuTable mt15 = new MenuTable();
            //mt15.MeunId = "PaymentSearch";
            //mt15.Meunlevel = 1;
            //mt15.MeunName = "缴款单查询";
            //mt15.MeunPic = "favorites.png";
            //mt15.MeunShortcuts = "01";
            //mt15.ParentMeunId = "PaymentManager";
            //mt15.MeunDll = "Payment.dll";
            //mt15.MeunClass = "Payment.Run";
            //mt15.MeunMethod = "SearchShow";
            //mtList.Add(mt15);


            //MenuTable mt16 = new MenuTable();
            //mt16.MeunId = "MemberManager";
            //mt16.Meunlevel = 0;
            //mt16.MeunName = "会员管理";
            //mt16.MeunPic = "favorites.png";
            //mt16.MeunShortcuts = "";
            //mtList.Add(mt16);
            //MenuTable mt17 = new MenuTable();
            //mt17.MeunId = "AddMember";
            //mt17.Meunlevel = 1;
            //mt17.MeunName = "添加会员";
            //mt17.MeunPic = "favorites.png";
            //mt17.MeunShortcuts = "01";
            //mt17.ParentMeunId = "MemberManager";
            //mt17.MeunDll = "Member.dll";
            //mt17.MeunClass = "Member.Run";
            //mt17.MeunMethod = "Show";
            //mtList.Add(mt17);

            //MenuTable mt18 = new MenuTable();
            //mt18.MeunId = "MemberSearch";
            //mt18.Meunlevel = 1;
            //mt18.MeunName = "会员查询";
            //mt18.MeunPic = "favorites.png";
            //mt18.MeunShortcuts = "01";
            //mt18.ParentMeunId = "MemberManager";
            //mt18.MeunDll = "Member.dll";
            //mt18.MeunClass = "Member.Run";
            //mt18.MeunMethod = "SearchShow";
            //mtList.Add(mt18);

            //MenuTable mt19 = new MenuTable();
            //mt19.MeunId = "DeliverySearch";
            //mt19.Meunlevel = 1;
            //mt19.MeunName = "发货单查询";
            //mt19.MeunPic = "favorites.png";
            //mt19.MeunShortcuts = "06";
            //mt19.ParentMeunId = "StockManager";
            //mt19.MeunDll = "GoodsDelivery.dll";
            //mt19.MeunClass = "GoodsDelivery.Run";
            //mt19.MeunMethod = "SearchShow";
            //mtList.Add(mt19);
            //return mtList;
            #endregion

            #region 菜单数据加载
            List<MenuTable> menuList = null;
            try
            {
                if (!string.IsNullOrEmpty(LoginInfo.UserLoginId))
                {
                    var searchCondition = new
                    {
                        userLoginId = LoginInfo.UserLoginId
                    };
                    if (DevCommon.getDataByWebService("Menu", "Menu", searchCondition, ref menuList) == RetCode.OK)
                    {
                        return menuList;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion

            
        }
        public static List<MenuComposite> GetMenuTree() 
        {
            List<MenuTable> mtList = GetMenuData();
            List<MenuComposite> comList = new List<MenuComposite>();
            foreach (MenuTable menuTable in mtList)
            {
                if (menuTable.Meunlevel == 0)
                {
                    MenuComposite com = new MenuComposite(menuTable);
                    addSubMenu(com, mtList);
                    comList.Add(com);
                }
            }
            return comList;
        }
        public static void addSubMenu(MenuComposite com, List<MenuTable> mtList)
        {
            foreach (MenuTable menuTable in mtList)
            {
                if (menuTable.ParentMeunId != null && menuTable.ParentMeunId.Equals(com.MeunId))
                {
                    //MenuComposite com = new MenuComposite(menuTable);
                    if (string.IsNullOrEmpty(menuTable.MeunDll))
                    {
                        MenuComposite com1 = new MenuComposite(menuTable);
                        addSubMenu(com1, mtList);
                        com.add(com1);
                    }
                    else
                    {
                        com.add(menuTable);
                    }
                }
            }
        }
    }
}
