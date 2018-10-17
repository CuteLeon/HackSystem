using System;
using System.IO;

namespace HackSystem.Host
{
    /// <summary>
    /// 文件系统控制器
    /// </summary>
    public static class FileController
    {
        /// <summary>
        /// 格式化字节大小
        /// </summary>
        /// <param name="ByteCount">比特总数</param>
        /// <returns></returns>
        public static string FormatSize(ulong ByteCount)
        {
            string Result = "";
            if (ByteCount < 1024)
                Result = (ByteCount + " Byte");
            else if (ByteCount < 1048576)
                Result = string.Format("{0} KB", ByteCount >> 10);
            else if (ByteCount < 1073741824)
                Result = string.Format("{0} MB", ByteCount >> 20);
            else if (ByteCount < 1099511627776)
                Result = string.Format("{0} GB", ByteCount >> 30);
            else
                Result = string.Format("{0} TB", ByteCount >> 40);
            
            return Result;
        }

        /// <summary>
        /// 读取文件大小
        /// </summary>
        /// <returns>文件大小</returns>
        public static string GetFileSize(string FilePath)
        {
            try
            {
                return FormatSize(Convert.ToUInt64(new FileInfo(FilePath).Length));
            } catch 
            {
                return "(未知大小)";
            }
        }

        /// <summary>
        /// 安全的合并路径
        /// </summary>
        /// <param name="DirectoryPath"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string PathCombine(string DirectoryPath, string FileName)
        {
            if (!DirectoryPath.EndsWith("\\")) DirectoryPath += "\\";
            return (DirectoryPath + FileName).Replace("\\\\", "\\");
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="SourcesPath">源文件路径</param>
        /// <param name="TargetPath">目标文件路径</param>
        public static void CopyFile(string SourcesPath, string TargetPath, bool OverWrite, bool ThrowException)
        {
            LogController.Debug("复制文件 \"{0}\" 至 \"{1}\"，{2}允许复写，{3}允许抛出异常 ", SourcesPath, TargetPath, OverWrite ? "" : "不", ThrowException ? "" : "不");
            try
            {
                File.Copy(SourcesPath, TargetPath, OverWrite);
            }
            catch (Exception CopyException)
            {
                LogController.Error("复制文件遇到错误：{0}", CopyException.Message);
                if (ThrowException) throw CopyException;
            }
        }

        /// <summary>
        /// 复制目录
        /// </summary>
        /// <param name="SourceDirectory">源目录</param>
        /// <param name="TargetDirectory">目标目录</param>
        /// <param name="CopyChildDir">是否复制子目录</param>
        /// <returns>返回 (总目录数, 成功数, 总文件数, 成功数)</returns>
        public static Tuple<int, int, int, int> CopyDirectory(string SourceDirectory, string TargetDirectory, bool CopyChildDir)
        {
            LogController.Debug("复制目录：{0} => {1}", SourceDirectory, TargetDirectory);
            //依次对应返回值
            int DirCount = 0, DirOKCount = 0, FileCount = 0, FileOKCount = 0;

            if (!Directory.Exists(SourceDirectory))
            {
                LogController.Warn("源目录 {0} 不存在，无法复制", SourceDirectory);
                return new Tuple<int, int, int, int>(0, 0, 0, 0);
            }

            if (!Directory.Exists(TargetDirectory))
            {
                try
                {
                    Directory.CreateDirectory(TargetDirectory);
                }
                catch (Exception ex)
                {
                    LogController.Error("创建目录 {0} 失败：{1}", TargetDirectory, ex.Message);
                    return new Tuple<int, int, int, int>(0, 0, 0, 0);
                }
            }

            DirOKCount++;

            //复制文件
            foreach (string FilePath in Directory.GetFiles(SourceDirectory))
            {
                LogController.Debug("复制文件：{0}", FilePath);
                try
                {
                    File.Copy(FilePath, PathCombine(TargetDirectory, Path.GetFileName(FilePath)), true);
                    FileOKCount++;
                }
                catch (Exception ex)
                {
                    LogController.Error("复制文件遇到错误：{0}", ex.Message);
                }
                finally
                {
                    FileCount++;
                }
            }

            if (CopyChildDir)
            {
                //复制子目录
                foreach (string ChildDirectoryPath in Directory.GetDirectories(SourceDirectory))
                {
                    DirCount++;
                    Tuple<int, int, int, int> CopyResult = CopyDirectory(ChildDirectoryPath, PathCombine(TargetDirectory, Path.GetFileName(ChildDirectoryPath)), CopyChildDir);
                    DirCount += CopyResult.Item1;
                    DirOKCount += CopyResult.Item2;
                    FileCount += CopyResult.Item3;
                    FileOKCount += CopyResult.Item4;
                    LogController.Debug("复制子目录：{0}\n\t\t\t\t\t\t\t     DirCount:{1}    DirOKCount:{2}    FileCount:{3}    FileOKCount:{4}", ChildDirectoryPath, CopyResult.Item1, CopyResult.Item2, CopyResult.Item3, CopyResult.Item4);
                }
            }

            DirOKCount++;
            return new Tuple<int, int, int, int>(DirCount, DirOKCount, FileCount, FileOKCount);
        }

    }
}
