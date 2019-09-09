<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="../helper.xslt" />
  <xsl:output method="html" indent="no" />

  <xsl:template match="cs_custsubinsnickname">
    <table class="tblDesc">
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'SubAccName'" />
        <xsl:with-param name="firstValue" select="@subacc" />

        <xsl:with-param name="secondKey" select="'nickname'" />
        <xsl:with-param name="secondValue" select="xslLogFormat:FormatNickName(@username2)" />

        <xsl:with-param name="thirdKey" select="'ErrCode'" />
        <xsl:with-param name="thirdValue" select="@errcode" />
      </xsl:call-template>
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'ErrMsg'" />
        <xsl:with-param name="firstValue" select="@errmsg" />

        <xsl:with-param name="secondKey" select="'IP'" />
        <xsl:with-param name="secondValue" select="xslLogFormat:FormatIP(@ip)" />
      </xsl:call-template>
    </table>
  </xsl:template>
</xsl:stylesheet>
