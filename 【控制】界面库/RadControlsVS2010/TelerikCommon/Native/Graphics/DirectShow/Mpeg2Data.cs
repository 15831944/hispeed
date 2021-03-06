using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Telerik.WinControls.UI.Multimedia
{
    #region Declarations

    /// <summary>
    /// From MPEG_REQUEST_TYPE
    /// </summary>
    internal enum MPEGRequestType
    {
        // Fields
        PES_STREAM = 6,
        SECTION = 1,
        SECTION_ASYNC = 2,
        SECTIONS_STREAM = 5,
        TABLE = 3,
        TABLE_ASYNC = 4,
        TS_STREAM = 7,
        START_MPE_STREAM = 8,
        UNKNOWN = 0
    }

    /// <summary>
    /// From MPEG_CONTEXT_TYPE
    /// </summary>
    internal enum MPEGContextType
    {
        BCSDeMux = 0,
        WinSock = 1
    }

    /// <summary>
    /// From MPEG_PACKET_LIST
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    internal struct MPEGPacketList
    {
        public short wPacketCount;
        public IntPtr PacketList;
    }

    /// <summary>
    /// From DSMCC_FILTER_OPTIONS
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    internal struct DSMCCFilterOptions
    {
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyProtocol;
        public byte Protocol;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyType;
        public byte Type;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyMessageId;
        public short MessageId;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyTransactionId;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fUseTrxIdMessageIdMask;
        public int TransactionId;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyModuleVersion;
        public byte ModuleVersion;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyBlockNumber;
        public short BlockNumber;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fGetModuleCall;
        public short NumberOfBlocksInModule;
    }

    /// <summary>
    /// From ATSC_FILTER_OPTIONS
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    internal struct ATSCFilterOptions
    {
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyEtmId;
        public int EtmId;
    }

    /// <summary>
    /// From MPEG2_FILTER
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    internal class MPEG2Filter
    {
        public byte bVersionNumber;
        public short wFilterSize;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fUseRawFilteringBits;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
        public byte[] Filter;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
        public byte[] Mask;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyTableIdExtension;
        public short TableIdExtension;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyVersion;
        public byte Version;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifySectionNumber;
        public byte SectionNumber;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyCurrentNext;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fNext;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyDsmccOptions;
        [MarshalAs(UnmanagedType.Struct)]
        public DSMCCFilterOptions Dsmcc;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fSpecifyAtscOptions;
        [MarshalAs(UnmanagedType.Struct)]
        public ATSCFilterOptions Atsc;
    }


    /// <summary>
    /// From unnamed union
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack=1)]
    internal struct MPEGContextUnion
    {
        // Fields
        [FieldOffset(0)]
        public BCSDeMux Demux;
        [FieldOffset(0)]
        public MPEGWinSock Winsock;
    }

    /// <summary>
    /// From MPEG_BCS_DEMUX
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    internal struct BCSDeMux
    {
        public int AVMGraphId;
    }

    /// <summary>
    /// From MPEG_WINSOCK
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    internal struct MPEGWinSock
    {
        public int AVMGraphId;
    }

    /// <summary>
    /// From MPEG_CONTEXT
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    internal class MPEGContext
    {
        public MPEGContextType Type;
        public MPEGContextUnion U;
    }

    /// <summary>
    /// From MPEG_STREAM_BUFFER
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    internal class MPEGStreamBuffer
    {
        //[MarshalAs(UnmanagedType.Error)]
        public int hr;
        public int dwDataBufferSize;
        public int dwSizeOfDataRead;
        public IntPtr pDataBuffer;
    }

    #endregion

    #region Interfaces

    [ComImport,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("9B396D40-F380-4E3C-A514-1A82BF6EBFE6")]
    internal interface IMpeg2Data
    {
        [PreserveSig]
        int GetSection(
            [In] short pid,
            [In] byte tid,
            [In] MPEG2Filter pFilter,
            [In] int dwTimeout,
            [MarshalAs(UnmanagedType.Interface)] out ISectionList ppSectionList
            );

        [PreserveSig]
        int GetTable(
            [In] short pid,
            [In] byte tid,
            [In] MPEG2Filter pFilter,
            [In] int dwTimeout,
            [MarshalAs(UnmanagedType.Interface)] out ISectionList ppSectionList
            );

        [PreserveSig]
        int GetStreamOfSections(
            [In] short pid,
            [In] byte tid,
            [In] MPEG2Filter pFilter,
            [In] IntPtr hDataReadyEvent,
            [MarshalAs(UnmanagedType.Interface)] out IMpeg2Stream ppMpegStream
            );
    }

    [ComImport,
    Guid("400CC286-32A0-4CE4-9041-39571125A635"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMpeg2Stream
    {
        [PreserveSig]
        int Initialize(
            [In] MPEGRequestType requestType,
            [In, MarshalAs(UnmanagedType.Interface)] IMpeg2Data pMpeg2Data,
            [In, MarshalAs(UnmanagedType.LPStruct)] MPEGContext pContext,
            [In] short pid,
            [In] byte tid,
            [In, MarshalAs(UnmanagedType.LPStruct)] MPEG2Filter pFilter,
            [In] IntPtr hDataReadyEvent
            );

        [PreserveSig]
        int SupplyDataBuffer(
            [In] MPEGStreamBuffer pStreamBuffer
            );
    }

    [ComImport,
    Guid("AFEC1EB5-2A64-46C6-BF4B-AE3CCB6AFDB0"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ISectionList
    {
        [PreserveSig]
        int Initialize(
            [In] MPEGRequestType requestType,
            [In, MarshalAs(UnmanagedType.Interface)] IMpeg2Data pMpeg2Data,
            [In, MarshalAs(UnmanagedType.LPStruct)] MPEGContext pContext,
            [In] short pid,
            [In] byte tid,
            [In, MarshalAs(UnmanagedType.LPStruct)] MPEG2Filter pFilter,
            [In] int timeout,
            [In] IntPtr hDoneEvent
            );

        [PreserveSig]
        int InitializeWithRawSections(
            [In] ref MPEGPacketList pmplSections
            );

        [PreserveSig]
        int CancelPendingRequest();

        [PreserveSig]
        int GetNumberOfSections(
            out short pCount
            );

        [PreserveSig]
        int GetSectionData(
            [In] short SectionNumber,
            [Out] out int pdwRawPacketLength,
            [Out] out IntPtr ppSection // PSECTION*
            );

        [PreserveSig]
        int GetProgramIdentifier(
            out short pPid
            );

        [PreserveSig]
        int GetTableIdentifier(
            out byte pTableId
            );
    }

    #endregion
}
