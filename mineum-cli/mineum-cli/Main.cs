using System;
using System.Net;
using System.Text;
using System.IO;
using System.Security.Cryptography;
namespace mineumcli
{
	class MainClass
	{
		public const string mc_ufile_url="http://127.0.0.1/mc_version";
	 static void Main (string[] args)
		{
			//MinecraftClient Xen = new.MinecraftClient;
			string act_ver = get_act_ver ();
			string cur_ver = get_cur_ver ();
			Console.WriteLine ("Current version:" + cur_ver + "\nActual version:" + act_ver);
			if (act_ver == cur_ver) {
				Console.WriteLine ("No updates available");
			} else if (act_ver != cur_ver) {
				Console.WriteLine ("Update available");
			}
			//md5_gen ();
			get_path ();
			Console.ReadLine ();
			
		}
	 static string get_path ()
		{
			switch (System.Environment.OSVersion.Platform) {
			case PlatformID.Unix:
				return "unix";
				break;
			case PlatformID.Win32NT:
				return "win";
				break;
			default:
				return "not supported";
				break;
			}
		}
	 static string get_act_ver ()
		{
			WebClient con = new WebClient ();
			byte[] udata = null;
			try {
				udata = con.DownloadData (mc_ufile_url);
				return Encoding.ASCII.GetString (udata);
			} catch (System.Net.WebException e) {
				return e.Message;
			}	
		}
	 static string get_cur_ver ()
		{
			string cur_ver_file = "/home/sergey/.minecraft/mc_version";
			FileStream f = new FileStream (cur_ver_file, FileMode.Open, FileAccess.Read);
			int length = (int)f.Length;
			byte[] buf = new byte[length];
			f.Read (buf, 0, length);
			return Encoding.ASCII.GetString(buf);
		}
	 static void md5_gen ()
		{
			string[] files = Directory.GetFiles ("/home/sergey/.minecraft", "*.*", SearchOption.AllDirectories);
			foreach (string file in files) {
				FileStream f = new FileStream (file, FileMode.Open, FileAccess.Read);
				int length = (int)f.Length;
				byte[] buf = new byte[length];
				f.Read (buf, 0, length);
				MD5 hasher = MD5.Create ();
				byte[] data = hasher.ComputeHash (buf);
				StringBuilder sBuilder = new StringBuilder ();
				for (int i = 0; i<data.Length; i++) {
					sBuilder.Append (data [i].ToString ("x2"));
				}
				
				Console.WriteLine (sBuilder.ToString()+" "+file);
			}
			
		}
	}
}
